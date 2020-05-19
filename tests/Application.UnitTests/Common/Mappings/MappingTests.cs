using AutoMapper;
using TvMazeScraper.Application.Common.Mappings;
using TvMazeScraper.Domain.Entities;
using NUnit.Framework;
using System;
using TvMazeScraper.Application.Shows.Queries.GetShows;

namespace TvMazeScraper.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
        
        [Test]
        [TestCase(typeof(ShowCast), typeof(CastPersonDto))]
        [TestCase(typeof(Show), typeof(ShowDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}
