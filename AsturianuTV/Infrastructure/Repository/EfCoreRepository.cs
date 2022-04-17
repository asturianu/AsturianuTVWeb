﻿using AsturianuTV.Infrastructure.Data;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsturianuTV.Infrastructure.Repository
{
    public class EfCoreRepository<T> : IRepository<T> where T : BaseEntity
    {
        public readonly AsturianuTVDbContext _context;

        public EfCoreRepository(AsturianuTVDbContext context) => _context = context;

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task AddRangeAsync(IList<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task<T> TryGetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (entity == null)
                throw new Exception("id not find");

            return entity;
        }

        public async Task<List<T>> ListAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public virtual IQueryable<T> Read() => _context.Set<T>().OrderBy(x => x.Id);

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            _context.SaveChanges();
            return entities;
        }
    }
}
