using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvMazeScraper.Application.Shows.Commands.ScrapeShows;
using TvMazeScraper.Application.Shows.Queries.GetShows;

namespace WebApi.Controllers
{
    [Route("shows")]
    public class ShowsController : ApiController
    {
        [HttpGet]
        public async Task<IEnumerable<ShowDto>> Get()
        {
            return await Mediator.Send(new GetShowsQuery());
        }

        [HttpPost, Route("scrapeSomeShows")]
        public async Task<ActionResult> Scrape(ScrapeShowsCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
