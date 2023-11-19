using Application.Core;
using Application.Interfaces;
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
    public class UpdateVisitor
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
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
                var listing = await _context.Listings
                    .Include(a =>a.Visitors).ThenInclude(u => u.AppUser)
                    .SingleOrDefaultAsync(x=> x.Id == request.Id);
               
                if (listing == null) return null;

                var user = await _context.Users.FirstOrDefaultAsync(x =>
                x.UserName == _userAccessor.GetUsername());

                if (user == null) return null;

                var hostUsername = listing.Visitors.FirstOrDefault(x=> x.IsCreator)?.AppUser.UserName;

                var visitation = listing.Visitors.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

                if (visitation != null && hostUsername == user.UserName)
                    listing.IsCancelled = !listing.IsCancelled;

                if (visitation != null && hostUsername != user.UserName)
                    listing.Visitors.Remove(visitation);

                if(visitation == null)
                {
                    visitation = new ListingVisitors
                    {
                        AppUser = user,
                        Listing = listing,
                        IsCreator = false
                    };

                    listing.Visitors.Add(visitation);

                }


                var result = await _context.SaveChangesAsync() > 0;
                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating visitation");


            }
        }

    }
}
