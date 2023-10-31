using AsturianuTV.Infrastructure.Data.Models;
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
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IList<T> entities);
        Task<List<T>> ListAsync(CancellationToken cancellationToken);
        IQueryable<T> Read();
    }
}
