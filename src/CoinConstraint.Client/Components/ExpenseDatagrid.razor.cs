using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Components;

public partial class ExpenseDatagrid
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
    public EventCallback<Expense> ExpenseDetailModalRequested { get; set; }

    [Parameter]
    public EventCallback NewExpenseDetailModalRequested { get; set; }

    private async Task HandleDeletedExpense(Expense expense)
    {
        await ExpenseDeleted.InvokeAsync(expense);
    }

    private async Task OpenPaymentURLAsync(Expense expense)
    {
        await ExpensePaymentRequested.InvokeAsync(expense);
    }

    private async Task OpenExpenseDetailModalModal(Expense expense)
    {
        await ExpenseDetailModalRequested.InvokeAsync(expense);
    }

    private async Task OpenNewExpenseDetailModalModal()
    {
        await NewExpenseDetailModalRequested.InvokeAsync();
    }
}
