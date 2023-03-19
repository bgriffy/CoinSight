namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

public class Note
{
    public int NoteID { get; set; }

    public int? BudgetID { get; set; }

    public string NoteText { get; set; } = string.Empty;

    [NotMapped]
    public bool IsNew { get; set; }
}
