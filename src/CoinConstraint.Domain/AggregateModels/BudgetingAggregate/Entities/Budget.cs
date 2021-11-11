using CoinConstraint.Domain.AggregateModels.UserAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate
{
    public class Budget
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; } = "";

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now;

        public List<Total> Totals { get; set; }

        public List<Bill> Bills { get; set; }

        public List<Expense> Expenses { get; set; }

        public Guid UUID { get; set; }

        public User User { get; set; }

        [NotMapped]
        public bool IsNew { get; set; }
    }
}
