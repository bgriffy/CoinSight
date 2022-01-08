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

        switch (context.Resource)
        {
            case Budget:
                budget = (Budget)context.Resource;
                break;
            case Expense:
                var expense = (Expense)context.Resource;
                budget = _budgetRepository.GetBudgetByID(expense.BudgetID);
                break;
            case Note:
                var note = (Note)context.Resource;
                budget = _budgetRepository.GetBudgetByID(note.BudgetID);
                break;
            case Reminder:
                var reminder = (Reminder)context.Resource;
                budget = _budgetRepository.GetBudgetByID(reminder.BudgetID);
                break; 
            default:
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
