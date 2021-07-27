using Core.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Storage.Interfaces;
using Storage.Models;
using System.Threading.Tasks;

namespace Core.Authorization
{
    public class AuditableModelAuthorizationHandler : AuthorizationHandler<SameCreatorRequirement, AuditableModel>
    {
        private readonly ILoggedUserProvider _loggedUserProvider;
        private readonly ILogger _logger;

        public AuditableModelAuthorizationHandler(ILoggedUserProvider loggedUserProvider, ILogger logger)
        {
            _loggedUserProvider = loggedUserProvider;
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameCreatorRequirement requirement, AuditableModel resource)
        {
            if(_loggedUserProvider.GetUserId() == resource.CreatorId || context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation(
                    "User{ id: " + _loggedUserProvider.GetUserId() +", name: " + context.User.Identity.Name + " } - tried perform unauthorize action");
            }

            return Task.CompletedTask;
        }
    }
}
