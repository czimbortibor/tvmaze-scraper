using System.Collections.Generic;
using System.Threading;
using TvMazeScraper.Application.Common.Models;

namespace TvMazeScraper.Application.Common.Interfaces
{
    public interface IScraper
    {
        IAsyncEnumerable<TvMazeShowModel> ScrapeSome(CancellationToken cancellationToken);
    }
}