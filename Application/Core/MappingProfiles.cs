using Application.Comments;
using Application.Listings;
using Application.Profiles;
using AutoMapper;
using Domain;

namespace Application.Core
{
    /// <summary>
    /// This class performs defenition for auto mapping.
    /// </summary>
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            string currentUsername = null;
            CreateMap<Listing, Listing>();
            CreateMap<Listing, ListingDto>()
                .ForMember(d => d.CreatorUsername, o => o.MapFrom(
                    s => s.Visitors.FirstOrDefault(x => x.IsCreator).AppUser.UserName));
            CreateMap<ListingVisitors, VisitorDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.AppUser.Followers.Count))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.AppUser.Followings.Count))
                .ForMember(d => d.Following,
                    o => o.MapFrom(s => s.AppUser.Followers.Any(x => x.Observer.UserName == currentUsername)));
            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(d => d.Image, s => s.MapFrom(o => o.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.Followers.Count))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.Followings.Count))
                .ForMember(d => d.Following,
                    o => o.MapFrom(s => s.Followers.Any(x => x.Observer.UserName == currentUsername)));

            CreateMap<Comment ,CommentDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<ListingVisitors, UserListingDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Listing.Id))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Listing.Address))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Listing.City))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Listing.Price))
                .ForMember(d => d.CreatorUsername, o => o.MapFrom(s =>
                    s.Listing.Visitors.FirstOrDefault(x => x.IsCreator).AppUser.UserName));


        }
    }
}
