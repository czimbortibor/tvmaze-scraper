using System;

namespace TvMazeScraper.Domain.Entities
{
    public class ShowCast
    {
        public int Id { get; set; }
        
        public int ShowId { get; set; }
        public Show Show { get; set; }
        
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
