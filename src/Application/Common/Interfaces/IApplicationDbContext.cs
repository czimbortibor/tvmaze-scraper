using TvMazeScraper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace TvMazeScraper.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Show> Shows { get; set; }
        
        DbSet<Person> Persons { get; set; }

        DbSet<ShowCast> Casts { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
