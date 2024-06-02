using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace NetProjeJWT.Users
{
    public class OverAgeRequirement : IAuthorizationRequirement
    {
        public int Age { get; set; }
    }

    public class OverAgeRequirementHandler : AuthorizationHandler<OverAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OverAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth)!.Value);

            var age = DateTime.Today.Year - dateOfBirth.Year;


            if (age >= requirement.Age)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
