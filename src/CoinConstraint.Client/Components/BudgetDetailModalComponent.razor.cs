using Blazorise;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.UserAggregate;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinConstraint.Client.Components
{
    public partial class BudgetDetailModalComponent
    {
        private Modal _budgetModal;
        private Budget _budget = new Budget();

        [Parameter]
        public EventCallback<Budget> NewBudgetAdded{get; set;}

        public void ShowNewBudget()
        {
            _budget = new Budget();
            _budget.IsNew = true;
            //TODO: Remove these. Leaving here for testing temporarily. 
            _budget.Bills = new List<Bill>();
            _budget.User = new User();
            _budget.Totals = new List<Total>();
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
            Close();
        }

    }
}
