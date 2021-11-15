using CoinConstraint.Domain.AggregateModels.UserAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate
{
    public class Budget
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; } = "";

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now;

        public List<Total> Totals { get; set; } = new List<Total>();    

        public List<Bill> Bills { get; set; } = new List<Bill>();

        public List<Expense> Expenses { get; set; } = new List<Expense>();

        public Guid UUID { get; set; }

        public User User { get; set; } = new User();

        public decimal BudgetedAmount { get; set; }

        public decimal ExpensedAmount { get; set; }

        [NotMapped]
        public bool IsNew { get; set; }

        [NotMapped]
        public bool IsUpdated { get; set; }
    }
}
