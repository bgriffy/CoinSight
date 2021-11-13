using Blazorise;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CoinConstraint.Client.Components;

public partial class ExpenseDetailComponent
{
    private Modal _expenseModal;
    private Expense _expense;

    [Parameter]
    public EventCallback<Expense> ExpenseAdded { get; set; }

    [Parameter]
    public EventCallback<Expense> ExpenseUpdated { get; set; }

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
            await ExpenseUpdated.InvokeAsync(_expense);
        }
        _expenseModal.Hide();
    }

}
