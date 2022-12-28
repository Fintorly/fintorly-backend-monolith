using System;
using System.Linq.Expressions;
using Fintorly.Domain.Common;
using Fintorly.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fintorly.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private FintorlyContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public GenericRepository(FintorlyContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(a => a.Id.Equals(id));
            if (entity == null)
                return false;
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<List<T>> GetAllAsync(bool isActive)
        {
            var result = await _context.Set<T>().Where(a => a.IsActive == isActive).ToListAsync();
            return result;
        }

        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            var result = filter == null
                ? await _context.Set<T>().ToListAsync()
                 : await _context.Set<T>().Where(filter).ToListAsync();

            return result;
        }

        public virtual async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter = null)
        {
            var result = await _context.Set<T>().SingleOrDefaultAsync(filter);
            return result;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var result= await _context.Set<T>().SingleOrDefaultAsync(a => a.Id.Equals(id));
            return result;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


