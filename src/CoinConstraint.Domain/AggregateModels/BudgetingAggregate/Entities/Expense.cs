namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

public class Expense
{

    [Key]
    public int? ID { get; set; } = null;

    public int BudgetID { get; set; } = 0;

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public bool Automatic { get; set; }

    public bool Pay { get; set; }

    public bool Paid { get; set; }

    public DateTime DueDate { get; set; } = DateTime.Now;

    public decimal Amount { get; set; } = 0;

    public string Note { get; set; } = "";

    [NotMapped]
    public bool IsNew { get; set; }

    [NotMapped]
    public bool IsUpdated { get; set; }

    public string PaymentURL { get; set; } = "";

    public Expense Clone()
    {
        var newExpense = new Expense()
        {
            ID = this.ID,
            BudgetID = this.BudgetID,
            Title = this.Title,
            Description = this.Description,
            Automatic = this.Automatic,
            Pay = this.Pay,
            Paid = this.Paid,
            DueDate = this.DueDate,
            Amount = this.Amount,
            Note = this.Note,
            PaymentURL = this.PaymentURL
        };

        return newExpense;
    }
}
