using TvMazeScraper.Application.Common.Interfaces;
using System;

namespace TvMazeScraper.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
