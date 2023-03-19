using CoinConstraint.Domain.AggregateModels.TransactionsAggregate.Entities;
using System;

namespace CoinConstraint.Application.Transactions;
public interface ITransactionService
{
    public Task<List<Transaction>> GetRecentTransactions(Guid uuid, DateTime startDate, DateTime endDate);
    public Task<List<Transaction>> GetTransactions(Guid uuid, CoinConstraint.Shared.Utils.TransactionFilterArgs filterArgs);
}
