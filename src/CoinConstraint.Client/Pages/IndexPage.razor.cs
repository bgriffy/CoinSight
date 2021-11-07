using Blazorise.DataGrid;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinConstraint.Client.Pages
{
    public partial class IndexPage
    {
        private List<Expense> _expenses;
        private Expense _selectedExpense; 

        protected override async Task OnInitializedAsync()
        {
            await BudgetingService.Init();
            LoadExpenses();
            StateHasChanged();
        }

        private void LoadExpenses()
        {
            _expenses = BudgetingService.GetAllExpenses();
            if((_expenses?.Count ?? 0) > 0)
            {
                _selectedExpense = _expenses[0];
            }
        }

        private void HandleNewExpense(SavedRowItem<Expense, Dictionary<string, object>> e)
        {
            e.Item.IsNew = true; 
        }

        private async Task SaveChanges()
        {
            await BudgetingService.SaveChanges();
        }
    }
}
