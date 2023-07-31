namespace CoinSight.Application.Expenses.Queries.GetExpenseByBudgetId;

public sealed record GetExpensesByBudgetId(int budgetId) : IQuery<ExpenseResponse>;