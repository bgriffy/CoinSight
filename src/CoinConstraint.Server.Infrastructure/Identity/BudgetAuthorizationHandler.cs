using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CoinConstraint.Server.Infrastructure.Identity;

public class BudgetAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Budget>
{
    private readonly IBudgetRepository _budgetRepository;

    public BudgetAuthorizationHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Budget resource)
    {
        var userID = context.User?.GetUserId();
        var userOwnsBudget = (Guid.Parse(userID) == resource.UUID);

        var creationIsAuthorized = (requirement == Operations.Create && userOwnsBudget);

        if (creationIsAuthorized)
        {
            context.Succeed(requirement);
        }

        var updateIsAuthorized = ((requirement == Operations.Update || requirement == Operations.Delete) && userOwnsBudget);

        if(updateIsAuthorized && _budgetRepository.BudgetExists(resource.ID, userID))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
