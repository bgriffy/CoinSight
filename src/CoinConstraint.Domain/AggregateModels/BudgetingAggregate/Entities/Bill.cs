namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

public class Bill
{
    [Key]
    public int ID { get; set; }

    public DateTime DueDate { get; set; } = DateTime.Now;
}
