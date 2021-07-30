using Core.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Storage.Interfaces;
using Storage.Models;
using System.Threading.Tasks;

namespace Core.Authorization
{
    public class AuditableModelAuthorizationHandler : AuthorizationHandler<SameCreatorRequirement, AuditableModel>
    {
        private readonly ILoggedUserProvider _loggedUserProvider;

        public AuditableModelAuthorizationHandler(ILoggedUserProvider loggedUserProvider)
        {
            _loggedUserProvider = loggedUserProvider;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameCreatorRequirement requirement, AuditableModel resource)
        {
            if(_loggedUserProvider.GetUserId() == resource.CreatorId || context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
