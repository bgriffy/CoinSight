namespace CoinConstraint.Shared.Utils;

public class TransactionFilterArgs
{
    public DateTime StartDate { get; set; } = DateTime.Now;

    public DateTime EndDate { get; set; } = DateTime.Now;

    public bool ShowIncomeTransactions { get; set; } = true;

    public bool ShowExpenseTransactions { get; set; } = true;

    public bool ShowCashTransactions { get; set; } = true;

    public bool ShowDebitTransactions { get; set; } = true;

    public bool ShowCreditTransactions { get; set; } = true;
}
