using Blazorise;

namespace CoinConstraint.Client.Components
{
    public partial class BudgetDetailModalComponent
    {
        private Modal _budgetModal;

        public void Show()
        {
            _budgetModal.Show();
        }

        public void Close()
        {
            _budgetModal.Hide();
        }

    }
}
