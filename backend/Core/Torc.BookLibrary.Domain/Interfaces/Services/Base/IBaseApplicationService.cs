using Domain.Entities.Base;
using System.Linq.Expressions;

namespace Domain.Interfaces.Services.Base;

public interface IBaseApplicationService<T> where T : BaseEntity
{
    public Task Insert(T entity);
    public Task Update(T entity);
    public Task<T> FindOne(Expression<Func<T, bool>> predicate);
}
