using Blazorise;
using Blazorise.DataGrid;
using CoinConstraint.Client.Components;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinConstraint.Client.Pages
{
    public partial class IndexPage
    {
        private List<Expense> _expenses;
        private Expense _selectedExpense;
        private List<Budget> _budgets;
        private Budget _selectedBudget; 
        private LoadSpinnerComponent _loadSpinner;
        private bool _pageIsLoaded = false;
        private BudgetDetailModalComponent _budgetModal;

        protected override async Task OnInitializedAsync()
        {
            await BudgetingService.Init();
            await LoadBudgets();
            await LoadExpenses();
            await _loadSpinner.HideLoadSpinner();
            StateHasChanged();
        }

        public async Task LoadBudgets()
        {
            await _loadSpinner.ShowLoadSpinner("Loading budgets...");
            _budgets = BudgetingService.GetAllBudgets();
            _selectedBudget = _budgets.FirstOrDefault();
        }

        private async Task LoadExpenses()
        {
            await _loadSpinner.ShowLoadSpinner("Loading expenses...");

            _expenses = BudgetingService.GetExpenses();

            if((_expenses?.Count ?? 0) > 0)
            {
                _selectedExpense = _expenses[0];
            }
        }

        public async Task HandleBudgetChange(int budgetID)
        {
            _selectedBudget = _budgets.FirstOrDefault(b => b.ID == budgetID);
            await BudgetingService.SetSelectedBudget(_selectedBudget);
            await LoadExpenses();
            await _loadSpinner.HideLoadSpinner();
            StateHasChanged();
        }

        private void HandleNewExpense(SavedRowItem<Expense, Dictionary<string, object>> e)
        {
            e.Item.IsNew = true;
            e.Item.BudgetID = _selectedBudget.ID;
        }

        private void HandleUpdatedExpense(SavedRowItem<Expense, Dictionary<string, object>> e)
        {
            e.Item.IsUpdated = true;
        }

        private void HandleDeletedExpense(Expense expense)
        {
            BudgetingService.MarkExpenseForDeletion(expense);
        }

        private void AddNewBudget()
        {
            _budgetModal.Show();
        }

        private async Task SaveChanges()
        {
            await _loadSpinner.ShowLoadSpinner("Saving changes...");
            await Task.Delay(5000);
            await BudgetingService.SaveExpenses();
            await _loadSpinner.HideLoadSpinner();
        }
    }
}
