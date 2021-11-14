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
        private ExpenseDetailComponent _expenseModal;
        private BudgetsModalComponent _budgetsModal;
        private bool _isDirty;

        protected override async Task OnInitializedAsync()
        {
            await BudgetingService.Init();
            await LoadData();
        }

        public async Task LoadData()
        {
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

        private void HandleNewExpense(Expense newExpense)
        {
            newExpense.BudgetID = _selectedBudget.ID;
            _expenses.Add(newExpense);
            _isDirty = true;
            StateHasChanged();
        }
        private void HandleUpdatedExpense(Expense updatedExpense)
        {
            _isDirty = true;
            StateHasChanged();
        }

        private void HandleDeletedExpense(Expense expense)
        {
            BudgetingService.MarkExpenseForDeletion(expense);
            _isDirty = true;
        }

        private void AddNewBudget()
        {
            _budgetModal.ShowNewBudget();
        }

        private async Task HandleNewBudget(Budget newBudget)
        {
            BudgetingService.AddNewBudget(newBudget);
            await LoadData();
        }

        private void AddNewExpense()
        {
            _expenseModal.ShowNewExpense();
        }

        private void EditExpense(Expense expense)
        {
            _expenseModal.ShowExpense(expense);
        }

        private void OpenBudgetManagement()
        {
            _budgetsModal.Show();
        }

        private async Task SaveBudgets()
        {
            await _loadSpinner.ShowLoadSpinner("Saving budgets...");
            await Task.Delay(1000);
            await BudgetingService.SaveBudgets();
            await _loadSpinner.HideLoadSpinner();
        }

        private async Task SaveChanges()
        {
            await _loadSpinner.ShowLoadSpinner("Saving changes...");
            await Task.Delay(1000);
            await BudgetingService.SaveChanges();
            await _loadSpinner.HideLoadSpinner();
        }
    }
}
