using System;

namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate
{
    public class Bill
    {
        public int ID { get; set; }

        public DateTime DueDate { get; set; }
    }
}
