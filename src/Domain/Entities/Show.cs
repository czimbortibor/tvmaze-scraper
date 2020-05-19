
using System.Collections.Generic;

namespace TvMazeScraper.Domain.Entities
{
    public class Show
    {
        public Show()
        {
            Cast = new HashSet<ShowCast>();
        }
        
        public int Id { get; set; }

        public string Title { get; set; }
        
        public ICollection<ShowCast> Cast { get; private set; }
    }
}
