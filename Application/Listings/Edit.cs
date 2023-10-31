using AutoMapper;
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
    /// This class performs a update a listing in the database useing Mediator pattern.
    /// this class define a querry and satisfy it by handler
    /// </summary>
    public class Edit
    {
        public class Command : IRequest
        {
            public Listing Listing { get; set; } 
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var listing = await _context.Listings.FindAsync(request.Listing.Id);
                _mapper.Map(request.Listing, listing);
                await _context.SaveChangesAsync();
                
            }
        }
    }
}
