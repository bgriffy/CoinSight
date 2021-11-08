using CoinConstraint.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoinConstraint.Server.Infrastructure.DataAccess
{
    public class ServersideRepository<T> : IServersideRepository<T> where T : class
    {
        private readonly DbSet<T> _dbset;
        private DbContext _dbContext;

        public ServersideRepository(DbContext dbContext)
        {
            _dbset = dbContext.Set<T>();
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbset.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbset.AddRangeAsync(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return (IEnumerable<T>)await _dbset.FindAsync(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public void Remove(T entity)
        {
            _dbset.Remove(entity);
        }

        public void RemoveAll()
        {
            _dbset.RemoveRange();
        }


        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbset.RemoveRange(entities);
        }


        public void Update(T entity)
        {
            _dbset.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
