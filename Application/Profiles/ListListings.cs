using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class ListListings
    {
        public class Query : IRequest<Result<List<UserListingDto>>>
        {
            public string Username { get; set; }
            public string Predicate { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<UserListingDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<UserListingDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.ListingVisters
                    .Where(u => u.AppUser.UserName == request.Username)
                    .OrderBy(a => a.Listing.DateTime)
                    .ProjectTo<UserListingDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();
                query = request.Predicate switch
                {
                    "creator" => query.Where(a => a.CreatorUsername == request.Username),
                    "visiting" => query.Where(a => a.CreatorUsername != request.Username),
                    _ => query

                };

                var listings = await query.ToListAsync();

                return Result<List<UserListingDto>>.Success(listings);
            }
        }
    }
}
