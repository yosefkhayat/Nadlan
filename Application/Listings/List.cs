using Application.Core;
using Application.Listings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    /// <summary>
    /// This class performs a call to database and return all the Listings available useing Mediator pattern.
    /// this class define a querry and satisfy it by handler
    /// </summary>
    public class List
    {
        public class Query : IRequest<Result<List<ListingDto>>> { }
        public class Handler : IRequestHandler<Query, Result<List<ListingDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<ListingDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var listings = await _context.Listings
                    .ProjectTo<ListingDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<List<ListingDto>>.Success(listings);
            }
        }
    }
}
