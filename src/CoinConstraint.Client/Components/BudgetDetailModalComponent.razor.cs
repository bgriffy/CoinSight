using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;
using Microsoft.AspNetCore.Components.Web;

namespace CoinConstraint.Client.Components;

public partial class BudgetDetailModalComponent
{
    private Blazorise.Modal _budgetModal;
    private Budget _budget = new Budget();

    [Parameter]
    public EventCallback<Budget> NewBudgetAdded { get; set; }

    [Parameter]
    public EventCallback BudgetModified { get; set; }

    public async Task HandleKeydownEvent(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await Save();
        }
    }

    public void ShowNewBudget(Budget newBudget = null)
    {
        if(newBudget == null)
        {
            _budget = new Budget();
            _budget.IsNew = true;
        }
        else
        {
            _budget = newBudget;
        }
        
        _budgetModal.Show();
    }

    public async void Show(Budget budget)
    {
        _budget = budget;
        _budgetModal.Show();
    }

    public void Close()
    {
        _budgetModal.Hide();
    }

    public async Task Save()
    {
        if (_budget.IsNew)
        {
            await NewBudgetAdded.InvokeAsync(_budget);
        }
        else
        {
            _budget.IsUpdated = true;
            await BudgetModified.InvokeAsync();
        }
        Close();
    }

}
