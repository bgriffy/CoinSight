using Blazorise;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinConstraint.Client.Components
{
    public partial class BudgetsModalComponent
    {
        private Modal _budgetsModal;
        private BudgetDetailModalComponent _budgetDetailModal;
        private Budget _selectedBudget;
        private bool _isDirty = false; 

        protected override void OnParametersSet()
        {
            if (Budgets == null) return;
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

        public void AddNewBudget()
        {
            _budgetDetailModal.ShowNewBudget();

        }

        public void EditBudget(Budget budget)
        {
            _budgetDetailModal.Show(_selectedBudget);
        }

        private async Task HandleNewBudget(Budget newBudget)
        {
            _isDirty = true;
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
            await BudgetDeleted.InvokeAsync(budget);
            StateHasChanged();
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
}
