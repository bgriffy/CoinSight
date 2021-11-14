using System.ComponentModel.DataAnnotations;

namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate
{
    public class Total
    {
        [Key]

        public int ID { get; set; }

        public int Title { get; set; } = 0;

        public int BudgetID { get; set; } = 0;
    }
}
