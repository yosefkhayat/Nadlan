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
    /// This class performs a call to with id of Listing to the database and delete it useing Mediator pattern.
    /// this class define a querry and satisfy it by handler
    /// </summary>
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; } 
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
                var listing = await _context.Listings.FindAsync(request.Id);
                _context.Remove(listing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
