namespace CoinConstraint.Domain.AggregateModels.UserAggregate.Entities;

public class User
{
    [Key]

    public Guid UUID { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Email { get; set; } = "";

    public string Username { get; set; } = "";

    public string Password { get; set; } = "";

    public List<Budget> Budgets { get; set; } = new List<Budget>();
}
