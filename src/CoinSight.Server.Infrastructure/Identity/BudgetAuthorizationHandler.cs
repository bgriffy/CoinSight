using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CoinConstraint.Server.Infrastructure.Identity;

public class BudgetAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IExpenseRepository _expenseRepository;
    private readonly INoteRepository _noteRepository;

    public BudgetAuthorizationHandler(IBudgetRepository budgetRepository, 
                                      IExpenseRepository expenseRepository, 
                                      INoteRepository noteRepository)
    {
        _budgetRepository = budgetRepository;
        _expenseRepository = expenseRepository;
        _noteRepository = noteRepository;
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
                var validExpense = ValidExpense(expense) || requirement == Operations.Create;
                if (!validExpense) return Task.CompletedTask;
                budget = _budgetRepository.GetBudgetByID(expense.BudgetID);
                break;
            case Note:
                var note = (Note)context.Resource;
                var validNote = ValidNote(note) || requirement == Operations.Create;
                if (!validNote) return Task.CompletedTask;
                budget = _budgetRepository.GetBudgetByID(note.BudgetID);
                break;
            case Reminder:
                var reminder = (Reminder)context.Resource;
                budget = _budgetRepository.GetBudgetByID(reminder.BudgetID);
                break; 
            default:
                return Task.CompletedTask;
        }

        DetermineIfOperationIsAuthorized(context, budget, requirement);

        return Task.CompletedTask;
    }

    private void DetermineIfOperationIsAuthorized(AuthorizationHandlerContext context, Budget budget, OperationAuthorizationRequirement requirement)
    {
        var userID = context.User?.GetUserId();
        var userOwnsBudget = (Guid.Parse(userID) == budget.UUID);

        var creationIsAuthorized = (requirement == Operations.Create && userOwnsBudget);

        if (creationIsAuthorized)
        {
            context.Succeed(requirement);
        }

        var updateIsAuthorized = ((requirement == Operations.Update || requirement == Operations.Delete) && userOwnsBudget);
        var readIsAuthorized = ((requirement == Operations.Read) && userOwnsBudget);

        if ((updateIsAuthorized || readIsAuthorized) && ValidBudget(budget, userID))
        {
            context.Succeed(requirement);
        }
    }

    private bool ValidBudget(Budget budget, string userID)
    {
        var validBudget = _budgetRepository.BudgetExists(budget.ID, userID);
        if (!validBudget) return false;
        return true;
    }

    private bool ValidExpense(Expense expense)
    {
        var validExpense = _expenseRepository.ExpenseExists(expense.ID, expense.BudgetID);
        if (!validExpense) return false;
        return true;
    }

    private bool ValidNote(Note note)
    {
        var validNote = _noteRepository.NoteExists(note.NoteID, note.BudgetID);
        if (!validNote) return false;
        return true;
    }
}
