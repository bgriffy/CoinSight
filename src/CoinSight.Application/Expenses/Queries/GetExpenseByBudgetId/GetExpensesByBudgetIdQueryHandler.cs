using CoinConstraint.Application.DataAccess;

namespace CoinSight.Application.Expenses.Queries.GetExpenseByBudgetId;

internal sealed class GetExpensesByBudgetIdQueryHandler
    : IQueryHandler<GetExpensesByBudgetId, ExpenseResponse>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICCUnitOfWork _unitOfWork;

    public GetExpensesByBudgetIdQueryHandler(IExpenseRepository expenseRepository, ICCUnitOfWork unitOfWork)
    {
        _expenseRepository = expenseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ExpenseResponse>> Handle(
        GetExpensesByBudgetId request,
        CancellationToken cancellationToken)
    {
        await Task.Yield();

        var budgetExists = _unitOfWork.Budgets.BudgetExists(request.budgetId);

        if (!budgetExists)
        {
            return Result.Failure<ExpenseResponse>(new Error(
                "Budget.NotFound",
                $"The budget with Id {request.budgetId} was not found"));
        }

        var expenses = _expenseRepository.GetExpensesByBudget(request.budgetId);

        var response = new ExpenseResponse(expenses);

        return response;
    }
}