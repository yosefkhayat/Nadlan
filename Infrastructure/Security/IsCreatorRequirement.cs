using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Claims;


namespace Infrastructure.Security
{
    public class IsCreatorRequirement:IAuthorizationRequirement
    { 
    }

    public class IsCreatorRequirementHandler : AuthorizationHandler<IsCreatorRequirement>
    {
        private readonly DataContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IsCreatorRequirementHandler(DataContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsCreatorRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return Task.CompletedTask;
            
            var ListingId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value?.ToString());

            var visitor = _dbContext.ListingVisters
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.AppUserId == userId && x.ListingId == ListingId)
                .Result;

            if(visitor== null) return Task.CompletedTask;

            if (visitor.IsCreator) context.Succeed(requirement);
            
            return Task.CompletedTask;

        }
    }
}
