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

namespace Application
{
    /// <summary>
    /// This class performs a call to database and return all the Listings available useing Mediator pattern.
    /// this class define a querry and satisfy it by handler
    /// </summary>
    public class List
    {
        public class Query : IRequest<Result<List<Listing>>> { }
        public class Handler : IRequestHandler<Query, Result<List<Listing>>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<Listing>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<Listing>>.Success(await _context.Listings.ToListAsync(cancellationToken));
            }
        }
    }
}
