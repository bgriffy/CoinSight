using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Components;

public partial class ExpenseDatagridComponent
{
    private bool _isSmallScreen;
    private bool _isMediumScreen;
    private bool _isLargeScreen;

    [Parameter]
    public Expense SelectedExpense { get; set; }

    [Parameter]
    public Budget SelectedBudget { get; set; }

    [Parameter]
    public EventCallback<Expense> ExpenseDeleted { get; set; }

    [Parameter]
    public EventCallback<Expense> ExpensePaymentRequested { get; set; }

    [Parameter]
    public EventCallback<Expense> ExpenseDetailRequested { get; set; }

    [Parameter]
    public EventCallback NewExpenseDetailRequested { get; set; }

    private async Task HandleDeletedExpense(Expense expense)
    {
        await ExpenseDeleted.InvokeAsync(expense);
    }

    private async Task OpenPaymentURLAsync(Expense expense)
    {
        await ExpensePaymentRequested.InvokeAsync(expense);
    }

    private async Task OpenExpenseDetailModal(Expense expense)
    {
        await ExpenseDetailRequested.InvokeAsync(expense);
    }

    private async Task OpenNewExpenseDetailModal()
    {
        await NewExpenseDetailRequested.InvokeAsync();
    }
}
