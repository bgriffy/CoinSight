﻿using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinConstraint.Application.Budgeting
{
    public interface IBudgetingService
    {
        List<Expense> GetAllExpenses();
        Task<List<Expense>> GetExpensesByBudget(int budgetID);
        Task Init();
        void MarkExpenseForDeletion(Expense expense);
        Task SaveExpenses();
    }
}