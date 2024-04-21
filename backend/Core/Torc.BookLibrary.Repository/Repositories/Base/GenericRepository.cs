using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Repository.Data;
using System.Linq.Expressions;
using Torc.BookLibrary.Domain.Interfaces.Repositories;

namespace Repository.Repositories.Base;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet;

    public GenericRepository(BookLibraryContext context)
    {
        _dbSet = context.Set<T>();
    }

    public IQueryable<T> Get()
    {
        return _dbSet;
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        entity.Update();
        _dbSet.Update(entity);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.CountAsync(expression);
    }

    public async Task<T> FirstAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.FirstOrDefaultAsync(expression);
    }

    public async Task<T> FirstWithIncludeOrderAsync(Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        Expression<Func<T, object>> orderBy = null)
    {
        var query = _dbSet.AsQueryable();

        if (expression != null)
            query = query.Where(expression);

        if (include != null)
            query = include(query);

        if (orderBy != null)
            query = query.OrderByDescending(orderBy);

        query.OrderBy(x => x);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> GetDataAsync(Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        Expression<Func<T, object>> orderBy = null,
        int? skip = null,
        int? take = null)
    {
        var query = _dbSet.AsQueryable();

        if (expression != null)
            query = query.Where(expression);

        if (include != null)
            query = include(query);

        if (orderBy != null)
            query = query.OrderByDescending(orderBy);

        if (skip != null && skip.HasValue)
            query = query.Skip(skip.Value);

        if (take != null && take.HasValue)
            query = query.Take(take.Value);

        query.OrderBy(x => x);

        return await query.AsNoTracking().ToListAsync();
    }
}
