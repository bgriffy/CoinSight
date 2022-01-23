using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace CoinConstraint.Application.Identity;

public interface ICCAuthorizationService
{
    Task<bool> ActionIsAuthorized(ClaimsPrincipal user, object resource, OperationAuthorizationRequirement requirement);
}
