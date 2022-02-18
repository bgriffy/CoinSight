using Blazor.ModalDialog;
using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;
using Microsoft.AspNetCore.Components.Web;

namespace CoinConstraint.Client.Components.Budgets;

public partial class BudgetDetailModal
{
    private Blazorise.Modal _budgetModal;
    private Budget _budget = new Budget();
    private bool _isDirty = false;

    [Parameter]
    public EventCallback<Budget> NewBudgetAdded { get; set; }

    [Parameter]
    public EventCallback BudgetModified { get; set; }

    public string BudgetTitle
    {
        get => _budget.Title;
        set
        {
            _budget.Title = value;
            _isDirty = true;
        }
    }

    public DateTime BudgetStartDate
    {
        get => _budget.StartDate;
        set
        {
            _budget.StartDate = value;
            _isDirty = true;
        }
    }

    public DateTime BudgetEndDate
    {
        get => _budget.EndDate;
        set
        {
            _budget.EndDate = value;
            _isDirty = true;
        }
    }

    public async Task HandleKeydownEvent(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await Save();
        }
    }

    public void ShowNewBudget(Budget newBudget = null)
    {
        if (newBudget == null)
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

    public void MarkAsDirty()
    {
        _isDirty = true;
    }

    public async Task Show(Budget budget)
    {
        _budget = budget;
        await _budgetModal.Show();
    }

    private async Task OnModalClosing(ModalClosingEventArgs e)
    {
        if (_isDirty)
        {
            var tmpMessage = "You have un-saved changes on this budget. Do you wish to continue without saving?";
            MessageBoxDialogResult result = await ModalDialog.ShowMessageBoxAsync("Coin Constraint", tmpMessage, MessageBoxButtons.YesNo);
            if (result == MessageBoxDialogResult.No) e.Cancel = true;
        }
    }

    public async Task Close()
    {
        await _budgetModal.Hide();
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

        _isDirty = false;
        await Close();
    }

}
