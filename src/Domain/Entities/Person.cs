using System;

namespace TvMazeScraper.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}
