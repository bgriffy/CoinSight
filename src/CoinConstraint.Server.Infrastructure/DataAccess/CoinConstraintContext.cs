namespace CoinConstraint.Server.Infrastructure.DataAccess;

public class CoinConstraintContext : DbContext
{
    public CoinConstraintContext(DbContextOptions<CoinConstraintContext> options) : base(options)
    {

    }

    public DbSet<Bill> Bills { get; set; }

    public DbSet<Budget> Budgets { get; set; }

    public DbSet<Expense> Expenses { get; set; }

    public DbSet<Reminder> Reminders { get; set; }

    public DbSet<Total> Totals { get; set; }

    public DbSet<User> Users { get; set; }
}
