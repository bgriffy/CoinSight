using CoinConstraint.Application.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Client;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories.Clientside;

namespace CoinConstraint.Client.Infrastructure.DataAccess
{
    public class ClientsideCCUnitOfWork : IClientsideCCUnitOfWork
    {
        public ClientsideCCUnitOfWork(IClientsideBillRepository billRepository,
                                      IClientsideBudgetRepository budgetRepository,
                                      IClientsideExpenseRepository expenseRepository,
                                      IClientsideReminderRepository reminderRepository,
                                      IClientsideTotalRepository totalRepository,
                                      IClientsideUserRepository userRepository)
        {
            Bills = billRepository;
            Budgets = budgetRepository;
            Expenses = expenseRepository;
            Reminders = reminderRepository;
            Totals = totalRepository;
            Users = userRepository;
        }

        public IClientsideBillRepository Bills { get; set; }

        public IClientsideBudgetRepository Budgets { get; set; }

        public IClientsideExpenseRepository Expenses { get; set; }

        public IClientsideReminderRepository Reminders { get; set; }

        public IClientsideTotalRepository Totals { get; set; }

        public IClientsideUserRepository Users { get; }
    }
}
