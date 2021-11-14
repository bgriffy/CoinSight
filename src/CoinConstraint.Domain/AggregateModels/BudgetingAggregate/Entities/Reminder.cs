using CoinConstraint.Domain.AggregateModels.UserAggregate;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate
{
    public class Reminder
    {

        [Key]

        public int ID { get; set; }

        public string Subject { get; set; } = "";

        public string Body { get; set; } = "";

        public Guid UUID { get; set; }

        public User User { get; set; } = new User();

        public int BudgetID { get; set; } = 0;

        public Budget Budget { get; set; } =  new Budget();
    }
}
