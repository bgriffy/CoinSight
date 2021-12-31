using Microsoft.AspNetCore.Authorization;

namespace CoinConstraint.Server.Infrastructure.Identity;

public class BudgetAuthorizationHandler : AuthorizationHandler<BudgetAuthorRequirement, Budget>
{

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   BudgetAuthorRequirement requirement,
                                                   Budget resource)
    {
        var userID = Guid.Parse(context.User?.GetUserId());

        if (userID == resource.UUID)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
