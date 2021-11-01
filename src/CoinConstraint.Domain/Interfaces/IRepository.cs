using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoinConstraint.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void RemoveAll();
    }
}
