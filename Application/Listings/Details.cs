using Application.Core;
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

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<ListingDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var listing = await _context.Listings
                    .ProjectTo<ListingDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);
                return Result<ListingDto>.Success(listing);
            }
        }
    }
}
