using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;
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

        public async Task<bool> AuthorizeExpenseView(ClaimsPrincipal user, List<Expense> expenses)
        {
            foreach (var expense in expenses)
            {
                if (!(await ActionIsAuthorized(user, expense, Operations.Read)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
