using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinConstraint.Domain.Interfaces
{
    public interface IClientsideRepository<T> : IRepository<T> where T : class
    {
    }
}
