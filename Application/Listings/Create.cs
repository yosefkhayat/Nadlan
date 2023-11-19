using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Migrations;
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
        public class Command : IRequest<Result<Unit>>
        {
            public Listing Listing { get; set; }
        }

        public class CommandVlidator: AbstractValidator<Command>
        {
            public CommandVlidator() 
            {
                RuleFor(x => x.Listing).SetValidator(new ListingValidator());
            }
        }
        public class Handler : IRequestHandler<Command,Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x =>
                    x.UserName == _userAccessor.GetUsername());

                var visitor = new Domain.ListingVisitors
                {
                    AppUser = user,
                    Listing = request.Listing,
                    IsCreator = true

                };

                request .Listing.Visitors.Add(visitor);

                _context.Listings.Add(request.Listing);

                var result = await _context.SaveChangesAsync()>0;

                if (!result) return Result<Unit>.Failure("Failed to create listing!");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
