namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

public class Reminder
{

    [Key]

    public int ID { get; set; }

    public string Subject { get; set; } = "";

    public string Body { get; set; } = "";

    public Guid UUID { get; set; }

    public int BudgetID { get; set; } = 0;
}
