using Core.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Storage.Interfaces;
using Storage.Models;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ModifyAvilabilityChecker : IModifyAvalibilityChecker
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ILoggedUserProvider _loggedUserProvider;

        protected ModifyAvilabilityChecker(IAuthorizationService authorizationService, ILoggedUserProvider loggedUserProvider)
        {
            _authorizationService = authorizationService;
            _loggedUserProvider = loggedUserProvider;
        }

#nullable enable
        public async Task CheckCanBeModify(AuditableModel? model)
        {
            await CheckIsNotNull(model);
            await CheckAuthorization(model);
        }

        private Task CheckIsNotNull(AuditableModel? model)
        {
            if (model is null)
            {
                throw new NotFoundException("Entity does not exists");
            }

            return Task.CompletedTask;
        }

        private async Task CheckAuthorization(AuditableModel model)
        {
            AuthorizationResult authorizationResult = await _authorizationService.AuthorizeAsync(_loggedUserProvider.GetUserClaimsPrincipal(), model, "DeletePolicy");

            if (!authorizationResult.Succeeded)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
