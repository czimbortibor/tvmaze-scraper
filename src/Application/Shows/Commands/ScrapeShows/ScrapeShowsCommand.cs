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

        public ScrapeShowsCommandHandler(IScraper scraper)
        {
            _scraper = scraper;
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

            }

            return Unit.Value;
        }
    }
}