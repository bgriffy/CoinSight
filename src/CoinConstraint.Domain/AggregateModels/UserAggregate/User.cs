using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoinConstraint.Domain.AggregateModels.UserAggregate
{
    public class User
    {
        [Key]

        public Guid UUID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public List<Budget> Budgets { get; set; }
    }
}
