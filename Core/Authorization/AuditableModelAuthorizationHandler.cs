using Core.Authorization.Requirements;
using Core.Common;
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
       // private readonly ILogger _logger;

        public AuditableModelAuthorizationHandler(ILoggedUserProvider loggedUserProvider)
        {
            _loggedUserProvider = loggedUserProvider;
          //  _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameCreatorRequirement requirement, AuditableModel resource)
        {
            if(_loggedUserProvider.GetUserId() == resource.CreatorId || context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
            else
            {
            //    _logger.LogInformation(LoggerMessages.UnauthorizeOperation(_loggedUserProvider.GetUserId(), context.User.Identity.Name));
            }

            return Task.CompletedTask;
        }
    }
}
