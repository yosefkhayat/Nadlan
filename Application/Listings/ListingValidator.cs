using Domain;
using FluentValidation;

namespace Application.Listings
{
    public class ListingValidator : AbstractValidator<Listing>
    {
        public ListingValidator()
        {
            RuleFor(x=> x.Address).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Region).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
        }
    }
}
