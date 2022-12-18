using System;
using System.Linq.Expressions;

namespace Fintorly.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<IResult> GetAllAsync(bool isActive);
        Task<IResult> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<IResult> AddAsync(T entity);
        Task<IResult> GetByIdAsync(Guid Id);
        Task<IResult> GetByFilterAsync(Expression<Func<T, bool>> filter = null);
        Task<IResult> DeleteByIdAsync(Guid Id);
        Task<IResult> UpdateAsync(T entity);
    }
}

