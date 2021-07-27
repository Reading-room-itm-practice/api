using Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Storage.Interfaces;
using Storage.Models;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
    public abstract class AuthorizationBasedService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ILoggedUserProvider _loggedUserProvider;

        protected AuthorizationBasedService(IAuthorizationService authorizationService, ILoggedUserProvider loggedUserProvider)
        {
            _authorizationService = authorizationService;
            _loggedUserProvider = loggedUserProvider;
        }

#nullable enable
        protected async Task CheckCanBeModify(AuditableModel? model)
        {
            if (model is null)
            {
                throw new NotFoundException("Entity does not exists");
            }

            AuthorizationResult authorizationResult = await _authorizationService.AuthorizeAsync(_loggedUserProvider.GetUserClaimsPrincipal(), model, "DeletePolicy");

            if (!authorizationResult.Succeeded)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
