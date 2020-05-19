using AutoMapper;
using TvMazeScraper.Application.Common.Mappings;
using TvMazeScraper.Domain.Entities;

namespace TvMazeScraper.Application.Shows.Queries.GetShows
{
    public class CastPersonDto : IMapFrom<ShowCast>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShowCast, CastPersonDto>()
                .ForMember(x => x.Birthday,
                    opt => opt.MapFrom(s => s.Person.BirthDay.ToShortDateString()))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(s => s.Person.FullName));
        }
    }
}