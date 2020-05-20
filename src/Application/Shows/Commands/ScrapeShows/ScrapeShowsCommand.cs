using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TvMazeScraper.Application.Common.Interfaces;
using TvMazeScraper.Domain.Entities;

namespace TvMazeScraper.Application.Shows.Commands.ScrapeShows
{
    public class ScrapeShowsCommand : IRequest
    {
        
    }

    public class ScrapeShowsCommandHandler : IRequestHandler<ScrapeShowsCommand>
    {
        private readonly IScraper _scraper;
        private readonly IApplicationDbContext _dbContext;

        public ScrapeShowsCommandHandler(IScraper scraper, IApplicationDbContext dbContext)
        {
            _scraper = scraper;
            _dbContext = dbContext;
        }

        // proof of flow implementation
        public async Task<Unit> Handle(ScrapeShowsCommand request, CancellationToken cancellationToken)
        {
            await foreach (var model in _scraper.ScrapeSome(cancellationToken))
            {
                if (model == null)
                {
                    continue;
                }

                string showTitle = model.Name;
                if (await IsSavedShow(showTitle))
                {
                    continue;
                }
                
                int showEntityId = await CreateShow(showTitle);
                
               var personModels = 
                                model.Embedded.Cast.Select(x => (x.Person.Name, x.Person.BirthdayRawValue));
                            var personEntities = await GetPersons(personModels);

                await CreateCast(showEntityId, personEntities);
            }

            return Unit.Value;
        }

        private async Task<bool> IsSavedShow(string title)
        {
            return await _dbContext.Shows.AnyAsync(x => x.Title == title);
        }

        private async Task<int> CreateShow(string title)
        {
            var showEntity = new Show
            {
                Title = title
            };

            await _dbContext.Shows.AddAsync(showEntity);
            await _dbContext.SaveChangesAsync();

            return showEntity.Id;
        }

        private async Task<IEnumerable<Person>> GetPersons(IEnumerable<(string Name, DateTime? Birthday)> personValues)
        {
            if (personValues == null || personValues.Any() == false)
            {
                return Enumerable.Empty<Person>();
            }

            var personNames = personValues.Select(x => x.Name);
            
            var existingPersonNames= _dbContext.Persons.Select(x => x.FullName);
            var nonExistingPersonNames = personNames.Except(existingPersonNames);

            var personsToSave =
                personValues
                    .Where(x => nonExistingPersonNames.Contains(x.Name))
                    .Select(x => new Person
                    {
                        FullName = x.Name,
                        BirthDay = x.Birthday
                    })
                    .ToList();

            await _dbContext.Persons.AddRangeAsync(personsToSave);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Persons.Where(x => personNames.Contains(x.FullName)).ToListAsync();
        }

        private async Task CreateCast(int showId, IEnumerable<Person> persons)
        {
            var castEntities =
                persons.Select(x => new ShowCast
                    {
                        ShowId = showId,
                        PersonId = x.Id
                    })
                    .ToList();
            
            await _dbContext.Casts.AddRangeAsync(castEntities);
            await _dbContext.SaveChangesAsync();
        }
    }
}