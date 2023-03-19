using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace CoinConstraint.Server.Infrastructure.Identity
{
    public class CCAuthorizationService : ICCAuthorizationService
    {
        private readonly IAuthorizationService _authService;

        public CCAuthorizationService(IAuthorizationService authService)
        {
            _authService = authService;
        }

        public async Task<bool> ActionIsAuthorized(ClaimsPrincipal user, object resource, OperationAuthorizationRequirement requirement)
        {
            var authorizationResult = await _authService.AuthorizeAsync(user, resource, requirement);
            if (!authorizationResult.Succeeded) return false;

            return true;
        }

    }
}
