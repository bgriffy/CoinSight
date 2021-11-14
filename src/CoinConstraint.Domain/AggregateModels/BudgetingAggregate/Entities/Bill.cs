using System;
using System.ComponentModel.DataAnnotations;

namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate
{
    public class Bill
    {
        [Key]
        public int ID { get; set; }

        public DateTime DueDate { get; set; } = DateTime.Now;
    } 
}
