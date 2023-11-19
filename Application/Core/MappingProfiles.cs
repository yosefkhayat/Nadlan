using Application.Listings;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    /// <summary>
    /// This class performs defenition for auto mapping.
    /// </summary>
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Listing, Listing>();
            CreateMap<Listing, ListingDto>()
                .ForMember(d => d.CreatorUsername, o => o.MapFrom(s => s.Visitors
                .FirstOrDefault(x => x.IsCreator).AppUser.UserName));
            CreateMap<ListingVisitors, Profiles.Profile>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Userame, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio));
        }
    }
}
