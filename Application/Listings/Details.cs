using Application.Core;
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
        public class Query : IRequest<Result<Listing>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Listing>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<Listing>> Handle(Query request, CancellationToken cancellationToken)
            {
                var listing = await _context.Listings.FindAsync(request.Id);
                return Result<Listing>.Success(listing);
            }
        }
    }
}
