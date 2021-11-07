﻿using CoinConstraint.Application.Budgeting;
using CoinConstraint.Application.DataAccess;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinConstraint.Client.Infrastructure.Services
{
    public class BudgetingService : IBudgetingService
    {
        private readonly IClientsideCCUnitOfWork _unitOfWork;
        private List<Expense> _expenses;

        public BudgetingService(IClientsideCCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Init()
        {
            try
            {
                var expenses = await _unitOfWork.Expenses.GetAllAsync();
                _expenses = expenses.OrderBy(e => e.DueDate).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public List<Expense> GetAllExpenses()
        {
            return _expenses;
        }

        public List<Expense> GetExpensesByBudget(int budgetID)
        {
            return _expenses.Where(e => e.BudgetID == budgetID).ToList();
        }

        public async Task SaveChanges()
        {
            foreach (var expense in _expenses)
            {
                if (expense.IsNew)
                {
                    expense.Note = "";
                    expense.Paid = false;
                    expense.Pay = false;
                    expense.Automatic = false; 
                    expense.BudgetID = 2;
                    await _unitOfWork.Expenses.AddAsync(expense);
                }
            }
        }
    }
}