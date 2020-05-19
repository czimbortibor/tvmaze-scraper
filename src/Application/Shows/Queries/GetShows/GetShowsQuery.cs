using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TvMazeScraper.Application.Common.Interfaces;

namespace TvMazeScraper.Application.Shows.Queries.GetShows
{
    public class GetShowsQuery : IRequest<IEnumerable<ShowDto>>
    {
    }

    public class GetShowsQueryHandler : IRequestHandler<GetShowsQuery, IEnumerable<ShowDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetShowsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public Task<IEnumerable<ShowDto>> Handle(GetShowsQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}