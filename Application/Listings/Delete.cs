using Application.Core;
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
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; } 
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>?> Handle(Command request, CancellationToken cancellationToken)
            {
                var listing = await _context.Listings.FindAsync(request.Id);
                
                //if (listing == null) return null;

                _context.Remove(listing);
    
                var result = await _context.SaveChangesAsync()>0;

                if (!result) return Result<Unit>.Failure("Failed to delete the listing");
                
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
