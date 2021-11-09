using Blazorise.DataGrid;
using CoinConstraint.Client.Components;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinConstraint.Client.Pages
{
    public partial class IndexPage
    {
        private List<Expense> _expenses;
        private Expense _selectedExpense;
        private LoadSpinnerComponent _loadSpinner;
        private bool _expensesAreLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            await BudgetingService.Init();
            await LoadExpenses();
            StateHasChanged();
        }

        private async Task LoadExpenses()
        {
            await _loadSpinner.ShowLoadSpinner("Loading expenses...");
            _expenses = BudgetingService.GetAllExpenses();
            if((_expenses?.Count ?? 0) > 0)
            {
                _selectedExpense = _expenses[0];
            }
            await _loadSpinner.HideLoadSpinner();
        }

        private void HandleNewExpense(SavedRowItem<Expense, Dictionary<string, object>> e)
        {
            e.Item.IsNew = true;
            //TODO: Remove this eventually. For testing purposes temporarily. 
            e.Item.BudgetID = 2;
        }

        private void HandleUpdatedExpense(SavedRowItem<Expense, Dictionary<string, object>> e)
        {
            e.Item.IsUpdated = true;
        }

        private void HandleDeletedExpense(Expense expense)
        {
            BudgetingService.MarkExpenseForDeletion(expense);
        }

        private async Task SaveChanges()
        {
            await _loadSpinner.ShowLoadSpinner("Saving changes...");
            await BudgetingService.SaveExpenses();
            await _loadSpinner.HideLoadSpinner();
        }
    }
}
