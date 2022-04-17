﻿using AsturianuTV.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsturianuTV.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> TryGetByIdAsync(int id, CancellationToken cancellationToken);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        Task UpdateAsync(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        T Add(T entity);
        Task AddRangeAsync(IList<T> entities);
        Task<List<T>> ListAsync(CancellationToken cancellationToken);
        IQueryable<T> Read();
    }
}
