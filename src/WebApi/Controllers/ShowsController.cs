using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvMazeScraper.Application.Shows.Queries.GetShows;

namespace WebApi.Controllers
{
    [Authorize]
    public class TodoListsController : ApiController
    {
        [HttpGet]
        public async Task<IEnumerable<ShowDto>> Get()
        {
            return await Mediator.Send(new GetShowsQuery());
        }
    }
}
