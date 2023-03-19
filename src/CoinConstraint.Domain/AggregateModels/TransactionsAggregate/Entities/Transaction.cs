namespace CoinConstraint.Domain.AggregateModels.TransactionsAggregate.Entities;

public class Transaction
{
    [Key]
    public int? ID { get; set; } = null;

    public string Payee { get; set; }

    public string ExpenseDescription { get; set; }

    public DateTime TransactionDate { get; set; } = DateTime.Now;

    public decimal InflowAmount { get; set; } = 0;

    public decimal OutflowAmount { get; set; } = 0;

    public string PaymentMode { get; set; }

    public string Description { get; set; }

    public string Memo { get; set; }
}
