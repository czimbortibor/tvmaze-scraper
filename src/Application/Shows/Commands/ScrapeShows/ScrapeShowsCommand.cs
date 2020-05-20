using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TvMazeScraper.Application.Common.Interfaces;

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

                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}