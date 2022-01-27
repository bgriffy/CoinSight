using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Components;

public partial class BudgetsModal
{
    private Blazorise.Modal _budgetsModal;
    private BudgetDetailModal _budgetDetailModal;
    private Budget _selectedBudget;
    private bool _isDirty = false;
    private bool _isSmallScreen;
    private bool _isMediumScreen;
    private bool _isLargeScreen;

    protected override void OnParametersSet()
    {
        if (Budgets == null || _selectedBudget != null) return;
        _selectedBudget = Budgets.FirstOrDefault();
    }

    [Parameter]
    public List<Budget> Budgets { get; set; } = new List<Budget> { new Budget() };

    [Parameter]
    public EventCallback BudgetModified { get; set; }

    [Parameter]
    public EventCallback<Budget> BudgetDeleted { get; set; }

    [Parameter]
    public EventCallback<Budget> BudgetAdded { get; set; }

    [Parameter]
    public EventCallback BudgetsSaveRequested { get; set; }

    [Parameter]
    public EventCallback<Budget> BudgetCloneRequested { get; set; }

    public void AddNewBudget(Budget newBudget = null)
    {
        if(newBudget != null)
        {
            _budgetDetailModal.ShowNewBudget(newBudget);
        }
        else
        {
            _budgetDetailModal.ShowNewBudget();
        }

    }

    public void EditBudget(Budget budget)
    {
        _budgetDetailModal.Show(budget);
    }

    private async Task HandleNewBudget(Budget newBudget)
    {
        _isDirty = true;
        _selectedBudget = newBudget;
        await BudgetAdded.InvokeAsync(newBudget);
        StateHasChanged();
    }

    private async Task HandleModifiedBudget()
    {
        _isDirty = true;
        await BudgetModified.InvokeAsync();
        StateHasChanged();
    }

    private async Task HandleDeletedBudget(Budget budget)
    {
        _isDirty = true;
        _selectedBudget = null;
        await BudgetDeleted.InvokeAsync(budget);
        StateHasChanged();
    }

    private async Task CloneBudget(Budget budgetToClone)
    {
        await BudgetCloneRequested.InvokeAsync(budgetToClone);
        var newBudget = budgetToClone.Clone();
        _selectedBudget = newBudget;
        EditBudget(newBudget);
    }

    public void Show()
    {
        _budgetsModal.Show();
    }

    public void Close()
    {
        _budgetsModal.Hide();
    }

    private async Task Save()
    {
        Close();
        await BudgetsSaveRequested.InvokeAsync();
    }
}
