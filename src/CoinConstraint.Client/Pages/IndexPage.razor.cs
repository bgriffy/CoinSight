using Blazor.ModalDialog;
using CoinConstraint.Client.Components;
using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace CoinConstraint.Client.Pages
{
    public partial class IndexPage
    {
        private List<Budget> _budgets;
        private Budget _selectedBudget = new Budget();
        private Expense _selectedExpense;
        private Note _selectedNote;
        private LoadSpinner _loadSpinner;
        private ExpenseDetailModal _expenseModal;
        private BudgetsModal _budgetsModal;
        private SchedulingModal _schedulingModal;
        private NoteModal _noteModal;
        private BudgetScheduleDetailModal _budgetsScheduleModal;
        private string _budgetAmountText;
        private bool _isDirty;
        private bool _isSmallScreen; 
        private bool _isMediumScreen; 
        private bool _isLargeScreen;
        private bool _pageIsLoaded; 

        protected override async Task OnInitializedAsync()
        {
            await BudgetingService.Init();
            await LoadData();
        }

        public List<Budget> BudgetsForDropdown { get => _budgets.Where(b=> b.ID != 0).ToList(); }

        public async Task LoadData(Budget selectedBudget = null)
        {
            _pageIsLoaded = false;
            _isDirty = false;

            if(selectedBudget == null)
            {
                await LoadBudgets();
            }
            else
            {
                await LoadBudgets(selectedBudget);
            }

            await LoadExpenses();
            await _loadSpinner.HideLoadSpinner();
            _pageIsLoaded = true;
            StateHasChanged();
        }

        public async Task LoadBudgets(Budget selectedBudget = null)
        {
            await _loadSpinner.ShowLoadSpinner("Loading budgets...");
            _budgets = BudgetingService.GetAllBudgets();

            if(_budgets.Count > 0)
            {
                _selectedBudget = (selectedBudget != null && _budgets.Any(b=> b.ID == selectedBudget.ID) ? selectedBudget : _budgets.First());
            }

            if (_selectedBudget != null)
            {
                var notes = _selectedBudget.Notes;
                var rando = notes; 
            }

        }

        private async Task LoadExpenses()
        {
            await _loadSpinner.ShowLoadSpinner("Loading expenses...");

            if ((_selectedBudget?.Expenses?.Count ?? 0) > 0)
            {
                _selectedExpense = _selectedBudget.Expenses[0];
            }

            if (_selectedBudget != null)
            {
                _budgetAmountText = _selectedBudget.BudgetedAmount.ToString();
            }
        }

        private async Task OpenBudgetManagementModal()
        {
            if (_isDirty)
            {
                MessageBoxDialogResult result = await PromptForSave();
                if (result == MessageBoxDialogResult.No) return;
            }
            await _budgetsModal.Show();
        }

        public async Task OpenBudgetScheduleModal()
        {
            await _budgetsScheduleModal.Show();
        }

        public async Task OpenScheduleModal()
        {
             await _schedulingModal.Show();
        }

        private async Task<MessageBoxDialogResult> PromptForSave()
        {
            var tmpMessage = "You have un-saved changes on this budget. Do you wish to continue without saving?";
            MessageBoxDialogResult result = await ModalDialog.ShowMessageBoxAsync("Coin Constraint", tmpMessage, MessageBoxButtons.YesNo);
            return result;
        }

        private void OpenExpenseDetailModalModal()
        {
            _expenseModal.ShowNewExpense();
        }

        private void OpenNoteModalForNewNote()
        {
            _noteModal.Show();
        }

        private void DeleteNote(Note note)
        {
            BudgetingService.MarkNoteForDeletion(note);
            _selectedBudget.Notes.Remove(note);
        }

        private void DeleteSelectedNote()
        {
            BudgetingService.MarkNoteForDeletion(_selectedNote);
            _selectedBudget.Notes.Remove(_selectedNote);
        }

        private async Task OpenPaymentURLAsync(Expense expense)
        {
            await JSRuntime.InvokeAsync<object>("open", expense.PaymentURL, "_blank");
        }

        private void OpenExpenseDetailModalModal(Expense expense)
        {
            _expenseModal.ShowExpense(expense);
        }
        
        private void MarkSelectedBudgetAsDirty()
        {
            _isDirty = true;
            _selectedBudget.IsUpdated = true;
            Refresh();
        }

        private void HandleUpdatedBudgets()
        {
            _isDirty = true;
            Refresh();
        }

        private void Refresh()
        {
            StateHasChanged();
        }


        public async Task HandleBudgetChange(int? budgetID)
        {
            if (_isDirty)
            {
                MessageBoxDialogResult result = await PromptForSave();
                if (result == MessageBoxDialogResult.No) return;
            }

            _selectedBudget = _budgets.FirstOrDefault(b => b.ID == budgetID);
            await BudgetingService.SetSelectedBudget(_selectedBudget);
            await LoadData(_selectedBudget);
            await _loadSpinner.HideLoadSpinner();

            StateHasChanged();
        }

        private async Task HandleNewBudget(Budget newBudget)
        {
            BudgetingService.AddNewBudget(newBudget);
            await LoadData(newBudget);
        }

        private void HandleNewExpense(Expense newExpense)
        {
            newExpense.BudgetID = _selectedBudget.ID;
            _selectedBudget.Expenses.Add(newExpense);
            _selectedBudget.IsUpdated = true; 
            _isDirty = true;
            StateHasChanged();
        }

        private void HandleNewBudgetSchedule(BudgetSchedule budgetSchedule)
        {
            budgetSchedule.BudgetID = (int)_selectedBudget.ID;
            _selectedBudget.BudgetSchedules.Add(budgetSchedule);
            _selectedBudget.IsUpdated = true;
            _isDirty = true;
            StateHasChanged();
        }

        private void HandleNewNote(Note note)
        {
            note.BudgetID = _selectedBudget.ID;
            _selectedBudget.Notes.Add(note);
            _selectedBudget.IsUpdated = true;
            _isDirty = true;
            StateHasChanged();
        }

        private void HandleDeletedExpense(Expense expense)
        {
            if (expense.IsNew) return;
            BudgetingService.MarkExpenseForDeletion(expense);
            _selectedBudget.IsUpdated = true;
            _isDirty = true;
        }

        private void HandleDeletedSchedule(BudgetSchedule schedule)
        {
            BudgetingService.MarkScheduleForDeletion(schedule);
            _selectedBudget.IsUpdated = true;
            _isDirty = true;
        }

        private void HandleDeletedBudget(Budget budget)
        {
            BudgetingService.MarkBudgetForDeletion(budget);
            _isDirty = true;
        }

        private void HandleBudgetAmountChange(string text)
        {
            _budgetAmountText = text;
            _isDirty = true;
        }

        private void SyncData()
        {
            decimal newBudgetAmountValue = String.IsNullOrWhiteSpace(_budgetAmountText) ? 0 : decimal.Parse(_budgetAmountText);
            if (newBudgetAmountValue != _selectedBudget.BudgetedAmount)
            {
                _selectedBudget.IsUpdated = true;
                _selectedBudget.BudgetedAmount = newBudgetAmountValue;
            }
        }

        private async Task SaveBudgets()
        {
            _pageIsLoaded = false;
            await _loadSpinner.ShowLoadSpinner();
            await Task.Delay(1000);
            await BudgetingService.SaveChanges();
            await LoadData(_selectedBudget);
            _isDirty = false;
        }

        private async Task HandleSchedulingSave()
        {
            _selectedBudget.IsUpdated = true; 
            await SaveChanges();
        }

        private async Task SaveChanges()
        {
            _pageIsLoaded = false;
            await _loadSpinner.ShowLoadSpinner();
            SyncData();
            await Task.Delay(1000);
            await BudgetingService.SaveChanges(saveBudgetsOnly: false);
            await LoadData(_selectedBudget);
            _isDirty = false;
        }
    }
}
