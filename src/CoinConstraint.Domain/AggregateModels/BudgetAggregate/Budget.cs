using CoinConstraint.Domain.AggregateModels.UserAggregate;
using System;
using System.Collections.Generic;

namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate
{
    public class Budget
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<Total> Totals { get; set; }

        public List<Bill> Bills { get; set; }

        public List<Expense> Expenses { get; set; }

        public Guid UUID { get; set; }

        public User User { get; set; }
    }
}
