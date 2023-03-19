namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

public class Budget
{

    [Key]
    public int? ID { get; set; }

    public string Title { get; set; } = "";

    public DateTime StartDate { get; set; } = DateTime.Now;

    public DateTime EndDate { get; set; } = DateTime.Now;

    public List<Total> Totals { get; set; } = new List<Total>();

    public List<Bill> Bills { get; set; } = new List<Bill>();

    public List<Expense> Expenses { get; set; } = new List<Expense>();

    public Guid UUID { get; set; }

    public decimal BudgetedAmount { get; set; }

    public List<BudgetSchedule> BudgetSchedules { get; set; } = new List<BudgetSchedule>();

    public decimal ExpensedAmount
    {
        get => Expenses.Sum(e => e.Amount);
    }

    public List<Note> Notes { get; set; } = new List<Note>();

    [NotMapped]
    public string DateRange 
    { 
        get => $"{this.StartDate.ToString("MM/dd/yyyy")} - {this.EndDate.ToString("MM/dd/yyyy")}";
    }

    [NotMapped]
    public bool IsNew { get; set; }

    [NotMapped]
    public bool IsUpdated { get; set; }

    [NotMapped]
    public decimal TotalIncome { get; set; }

    [NotMapped]
    public decimal TotalExpense { get; set; }

    [NotMapped]
    public decimal TotalBalance { get; set; }

    [NotMapped]
    public decimal TotalTransactions { get; set; }

    public Budget Clone()
    {
        var newBudget = new Budget()
        {
            Title = Title,
            StartDate = StartDate,
            EndDate = EndDate,
            Totals = Totals,
            Bills = this.Bills.ToList(),
            UUID = UUID,
            BudgetedAmount = BudgetedAmount,
            IsNew = true
        };

        foreach (var expense in this.Expenses)
        {
            newBudget.Expenses.Add(new Expense()
            {
                BudgetID = (int)(newBudget.ID ?? 0),
                Amount = expense.Amount,
                Description = expense.Description,
                DueDate = expense.DueDate,
                Automatic = expense.Automatic,
                Note = expense.Note,
                Pay = expense.Pay,
                Title = expense.Title,
                PaymentURL = expense.PaymentURL
            });
        }

        foreach (var note in this.Notes)
        {
            newBudget.Notes.Add(new Note()
            {
                BudgetID = (int)(newBudget.ID ?? 0),
                NoteText = note.NoteText
            });
        }

        return newBudget;
    }
}
