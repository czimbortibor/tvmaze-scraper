using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TvMazeScraper.Application.Common.Interfaces;

namespace TvMazeScraper.Application.Shows.Queries.GetShows
{
    public class GetShowsQuery : IRequest<IEnumerable<ShowDto>>
    {
        public GetShowsQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
        
        public int Page { get; }
        public int PageSize { get; }
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
        
        public async Task<IEnumerable<ShowDto>> Handle(GetShowsQuery request, CancellationToken cancellationToken)
        {
            List<ShowDto> showDtos =
                await _dbContext.Shows
                    .Skip(request.Page * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<ShowDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            return showDtos;
        }
    }
}