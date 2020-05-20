using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TvMazeScraper.Application.Common.Models
{
    public class TvMazeShowModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public string Status { get; set; }
        public int Runtime { get; set; }
        public string Premiered { get; set; }
        public string OfficialSite { get; set; }
        public Rating Rating { get; set; }
        public int Weight { get; set; }
        public Network Network { get; set; }
        public Externals Externals { get; set; }
        public Image Image { get; set; }
        public string Summary { get; set; }
        public int Updated { get; set; }
        
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }
    
    public class Rating
    {
        public double Average { get; set; }
    }

    public class Country
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Timezone { get; set; }
    }

    public class Network
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
    }

    public class Externals
    {
        public int Tvrage { get; set; }
        public int Thetvdb { get; set; }
        public string Imdb { get; set; }
    }

    public class Image
    {
        public string Medium { get; set; }
        public string Original { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public string Birthday { get; set; }

        internal DateTime? BirthdayRawValue =>
            string.IsNullOrWhiteSpace(Birthday)
                ? (DateTime?)null
                : DateTime.Parse(Birthday);
        
        public string Gender { get; set; }
        public Image Image { get; set; }
    }

    public class Character
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
    }

    public class Cast
    {
        public Person Person { get; set; }
        public Character Character { get; set; }
        public bool Self { get; set; }
        public bool Voice { get; set; }
    }

    public class Embedded
    {
        public IEnumerable<Cast> Cast { get; set; }
    }
}