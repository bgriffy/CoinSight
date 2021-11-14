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
            _budget.IsNew = true;
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
