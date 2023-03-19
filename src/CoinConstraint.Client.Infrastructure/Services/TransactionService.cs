using CoinConstraint.Application.Transactions;
using CoinConstraint.Domain.AggregateModels.TransactionsAggregate.Entities;
using System;

namespace CoinConstraint.Client.Infrastructure.Services;
public class TransactionService : ITransactionService
{
    public async Task<List<Transaction>> GetRecentTransactions(Guid uuid, DateTime startDate, DateTime endDate)
    {
        await Task.Yield();

        var transactions = GetTransactions();

        return transactions.OrderByDescending(t => t.TransactionDate).ToList();
    }

    public async Task<List<Transaction>> GetTransactions(Guid uuid, TransactionFilterArgs filterArgs)
    {
        try
        {
            await Task.Yield();

            var transactions = GetTransactions();

            var filteredTransactions = transactions.Where(t => (t.TransactionDate >= filterArgs.StartDate && t.TransactionDate <= filterArgs.EndDate &&
                                                               (t.InflowAmount == 0 || filterArgs.ShowIncomeTransactions) &&
                                                               (t.OutflowAmount == 0 || filterArgs.ShowExpenseTransactions) &&
                                                               (t.PaymentMode.ToLower() != "cash" || filterArgs.ShowCashTransactions) &&
                                                               (t.PaymentMode.ToLower() != "credit" || filterArgs.ShowCreditTransactions) &&
                                                               (t.PaymentMode.ToLower() != "debit" || filterArgs.ShowDebitTransactions))).ToList();

            return filteredTransactions.OrderByDescending(t => t.TransactionDate).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error getting filtered transactions. Message: {e.Message}");
            throw;
        }
    }

    private List<Transaction> GetTransactions()
    {
        return new List<Transaction>
        {
            new (){ ID = 1, Description="Prog Auto", ExpenseDescription="Car Insurance", OutflowAmount=95.0M, Payee="Progressive", TransactionDate=DateTime.Now.AddDays(-5), PaymentMode="Debit"},
            new (){ ID = 1, Description="BBmortgage", ExpenseDescription="Mortgage", OutflowAmount=155.0M, Payee="Billy Bob Mortgage Services", TransactionDate=DateTime.Now.Date.AddDays(10), PaymentMode="Debit"},
            new (){ ID = 1, Description="StateFarmMonthly", ExpenseDescription="Home Insurance", OutflowAmount=80.00M, Payee="State Farm LLC", TransactionDate=DateTime.Now.Date.AddDays(-1), PaymentMode="Credit"},
            new (){ ID = 1, Description="VALELEC", ExpenseDescription="Electricity", OutflowAmount=95.0M, Payee="Valdosta Electric", TransactionDate=DateTime.Now.Date.AddDays(-10), PaymentMode="Cash"},
            new (){ ID = 1, Description="YoutubeSubscription", ExpenseDescription="Subscriptions Due First Half", OutflowAmount=22.0M, Payee="Google", TransactionDate=DateTime.Now.Date.AddDays(-13), PaymentMode="Cash"},
            new (){ ID = 1, Description="Walmart.com Pickup", ExpenseDescription="Groceries/Household", OutflowAmount=250.0M, Payee="Walmart", TransactionDate=DateTime.Now.Date.AddDays(-3), PaymentMode="Credit"},
            new (){ ID = 1, Description="Sams Club Pickup", ExpenseDescription="Groceries/Household", OutflowAmount=88.67M, Payee="SamsClub", TransactionDate=DateTime.Now.Date.AddDays(-4), PaymentMode="Credit"},
            new (){ ID = 1, Description="Steam Game Purchase", ExpenseDescription="Entertainment", OutflowAmount=60.55M, Payee="Steam LLC", TransactionDate=DateTime.Now.Date.AddDays(-15), PaymentMode="Credit"},
            new (){ ID = 1, Description="EA Games Transact", ExpenseDescription="Entertainment", OutflowAmount=50.44M, Payee="EA Games", TransactionDate=DateTime.Now.Date.AddDays(-8), PaymentMode="Credit"},
            new (){ ID = 1, Description="AutoZoneTransaction", ExpenseDescription="Automotive Purchases", OutflowAmount=22.56M, Payee="Autozone Inc", TransactionDate=DateTime.Now.Date.AddDays(1), PaymentMode="Cash"}
        };
    }

}
