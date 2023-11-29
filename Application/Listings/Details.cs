using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Listings
{
    public class Details
    {
        /// <summary>
        /// This class performs a call to with id of Listing to the database and return a Listings with the same id useing Mediator pattern.
        /// this class define a querry and satisfy it by handler
        /// </summary>
        public class Query : IRequest<Result<ListingDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListingDto>>
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
            public async Task<Result<ListingDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var listing = await _context.Listings
                    .ProjectTo<ListingDto>(_mapper.ConfigurationProvider,
                    new { currentUsername = _userAccessor.GetUsername() })
                    .FirstOrDefaultAsync(x => x.Id == request.Id);
                return Result<ListingDto>.Success(listing);
            }
        }
    }
}
