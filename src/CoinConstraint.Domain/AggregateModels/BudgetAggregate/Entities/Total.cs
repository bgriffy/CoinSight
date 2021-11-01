using System.ComponentModel.DataAnnotations;

namespace CoinConstraint.Domain.AggregateModels.BudgetAggregate
{
    public class Total
    {
        [Key]

        public int ID { get; set; }

        public int Title { get; set; }

        public int BudgetID { get; set; }
    }
}
