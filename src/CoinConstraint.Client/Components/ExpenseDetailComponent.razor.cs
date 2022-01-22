using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;
using Microsoft.AspNetCore.Components.Web;

namespace CoinConstraint.Client.Components;

public partial class ExpenseDetailComponent
{
    private Blazorise.Modal _expenseModal;
    private Expense _expense;

    [Parameter]
    public EventCallback<Expense> ExpenseAdded { get; set; }

    [Parameter]
    public EventCallback ExpenseUpdated { get; set; }

    public async Task HandleKeydownEvent(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await Save();
        }
    }

    protected override void OnInitialized()
    {
        _expense = new Expense();
    }

    public void ShowNewExpense()
    {
        _expense = new Expense();
        _expense.IsNew = true;
        _expenseModal.Show();
    }

    public void ShowExpense(Expense expense)
    {
        _expense = expense;
        _expenseModal.Show();
    }

    private async Task Save()
    {
        if (_expense.IsNew)
        {
            await ExpenseAdded.InvokeAsync(_expense);
        }
        else
        {
            _expense.IsUpdated = true;
            await ExpenseUpdated.InvokeAsync();
        }
        _expenseModal.Hide();
    }

}
