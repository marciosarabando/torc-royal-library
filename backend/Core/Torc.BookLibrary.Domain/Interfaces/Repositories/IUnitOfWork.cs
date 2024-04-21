using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Storage;

namespace Torc.BookLibrary.Domain.Interfaces.Repositories;
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GetRepository<T, I>() where T : BaseEntity where I : IGenericRepository<T>;
    Task<bool> SaveChangesAsync();
    void BeginTransaction();
    void Commit();
    void Rollback();
    IExecutionStrategy CreateExecutionStrategy();
    Task<IDbContextTransaction> BeginTransactionAsync();

}
