using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Torc.BookLibrary.Domain.Interfaces.Repositories;
public interface IGenericRepository<T> where T : BaseEntity
{
    IQueryable<T> Get();
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
    Task<T> GetByIdAsync(int id);
    Task<T> FirstAsync(Expression<Func<T, bool>> expression);
    Task<T> FirstWithIncludeOrderAsync(Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        Expression<Func<T, object>> orderBy = null);
    Task<int> CountAsync(Expression<Func<T, bool>> expression);
    Task<List<T>> GetDataAsync(Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        Expression<Func<T, object>> orderBy = null,
        int? skip = null,
        int? take = null);
}
