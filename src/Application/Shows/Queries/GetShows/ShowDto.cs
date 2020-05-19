using System.Collections.Generic;
using AutoMapper;
using TvMazeScraper.Application.Common.Mappings;
using TvMazeScraper.Domain.Entities;

namespace TvMazeScraper.Application.Shows.Queries.GetShows
{
    public class ShowDto : IMapFrom<Show>
    {
        public ShowDto()
        {
            Cast = new List<CastPersonDto>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        
        public IEnumerable<CastPersonDto> Cast { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Show, ShowDto>()
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(s => s.Title))
                .ForMember(x => x.Cast,
                    opt => opt.MapFrom(s => s.Cast));
        }
    }
}