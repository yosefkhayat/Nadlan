using Application.Core;
using Application.Interfaces;
using Application.Listings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application
{
    /// <summary>
    /// This class performs a call to database and return all the Listings available useing Mediator pattern.
    /// this class define a querry and satisfy it by handler
    /// </summary>
    public class List
    {
        public class Query : IRequest<Result<PagedList<ListingDto>>>
        {
            public ListingParams Params { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<PagedList<ListingDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _context = context;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            public async Task<Result<PagedList<ListingDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Listings
                    .OrderBy(d=> d.DateTime)
                    .ProjectTo<ListingDto>(_mapper.ConfigurationProvider,
                    new { currentUsername = _userAccessor.GetUsername() })
                    .AsQueryable() ;

                if (request.Params.IsVisiting && !request.Params.IsCreator)
                {
                    query = query.Where(x=>x.Visitors.Any(a=> a.Username == _userAccessor.GetUsername()) && x.CreatorUsername != _userAccessor.GetUsername());
                }

                if (request.Params.IsCreator && !request.Params.IsVisiting)
                {
                    query = query.Where(x=> x.CreatorUsername == _userAccessor.GetUsername() );
                }

                return Result<PagedList<ListingDto>>.Success(
                    await PagedList<ListingDto>.createAsync(query,request.Params.PageNumber,request.Params.PageSize)
                    );
            }
        }
    }
}
