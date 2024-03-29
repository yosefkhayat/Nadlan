﻿using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
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
        public class Command : IRequest<Result<Unit>>
        {
            public Listing Listing { get; set; } 
        }

        public class CommandVlidator : AbstractValidator<Command>
        {
            public CommandVlidator()
            {
                RuleFor(x => x.Listing).SetValidator(new ListingValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>?> Handle(Command request, CancellationToken cancellationToken)
            {
                var listing = await _context.Listings.FindAsync(request.Listing.Id);

                if (listing == null) return null;

                _mapper.Map(request.Listing, listing);

                var result =await _context.SaveChangesAsync()>0;

                if (!result) return Result<Unit>.Failure("Failed to update listing!");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
