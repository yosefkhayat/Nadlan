using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Listings
{
    /// <summary>
    /// This class performs creation of Listing in the database useing Mediator pattern.
    /// this class define a querry and satisfy it by handler
    /// </summary>
    public class Create
    {
        public class Command : IRequest
        {
            public Listing Listing { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Listings.Add(request.Listing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
