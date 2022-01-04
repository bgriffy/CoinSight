using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CoinConstraint.Server.Infrastructure.Identity;

public class BudgetAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>
{
    private readonly IBudgetRepository _budgetRepository;

    public BudgetAuthorizationHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement)
    {
        Budget budget;

        if(context.Resource is Budget) 
        { 
            budget = (Budget)context.Resource;
        }
        else if(context.Resource is Expense)
        {
            var expense = (Expense)context.Resource;
            budget = _budgetRepository.GetBudgetByID(expense.BudgetID);
        }
        else
        {
            return Task.CompletedTask;
        }

        var userID = context.User?.GetUserId();
        var userOwnsBudget = (Guid.Parse(userID) == budget.UUID);

        var creationIsAuthorized = (requirement == Operations.Create && userOwnsBudget);

        if (creationIsAuthorized)
        {
            context.Succeed(requirement);
        }

        var updateIsAuthorized = ((requirement == Operations.Update || requirement == Operations.Delete) && userOwnsBudget);
        var readIsAuthorized = ((requirement == Operations.Read) && userOwnsBudget);

        if ((updateIsAuthorized || readIsAuthorized) && _budgetRepository.BudgetExists(budget.ID, userID))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
