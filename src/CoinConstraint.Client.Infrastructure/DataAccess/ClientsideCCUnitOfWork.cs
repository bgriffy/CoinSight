using CoinConstraint.Application.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;

namespace CoinConstraint.Client.Infrastructure.DataAccess
{
    public class ClientsideCCUnitOfWork : IClientsideCCUnitOfWork
    {
        public ClientsideCCUnitOfWork(IBillRepository billRepository,
                                      IBudgetRepository budgetRepository,
                                      IExpenseRepository expenseRepository,
                                      IReminderRepository reminderRepository,
                                      ITotalRepository totalRepository,
                                      IUserRepository userRepository)
        {
            Bills = billRepository;
            Budgets = budgetRepository;
            Expenses = expenseRepository;
            Reminders = reminderRepository;
            Totals = totalRepository;
            Users = userRepository;
        }

        public IBillRepository Bills { get; set; }

        public IBudgetRepository Budgets { get; set; }

        public IExpenseRepository Expenses { get; set; }

        public IReminderRepository Reminders { get; set; }

        public ITotalRepository Totals { get; set; }

        public IUserRepository Users { get; }
    }
}
