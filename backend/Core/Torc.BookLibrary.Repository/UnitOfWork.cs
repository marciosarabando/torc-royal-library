using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Data;
using Repository.Repositories.Base;
using Torc.BookLibrary.Domain.Interfaces.Repositories;

namespace Repository;
public class UnitOfWork : IUnitOfWork
{
    private bool isDisposed;
    private readonly BookLibraryContext _context;
    private Dictionary<string, dynamic> _repositories = new();

    public UnitOfWork(BookLibraryContext context)
    {
        _context = context;
    }

    public void BeginTransaction()
    {
        _context.Database.BeginTransaction();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _context.Database.BeginTransactionAsync();
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return _context.Database.CreateExecutionStrategy();
    }

    public void Commit()
    {
        _context.Database.CommitTransaction();
    }

    public IGenericRepository<TEntity> GetRepository<TEntity, TInterface>()
        where TEntity : BaseEntity
        where TInterface : IGenericRepository<TEntity>
    {
        //RepositoryFactory
        _repositories ??= new Dictionary<string, dynamic>();
        var type = typeof(TEntity).Name;
        if (_repositories.ContainsKey(type))
            return _repositories[type];

        var repoType = typeof(TInterface);

        var implementation = typeof(GenericRepository<>).Assembly
                                                .ExportedTypes
                                                .First(x => !x.IsInterface
                                                            && x.Name.EndsWith("Repository")
                                                            && repoType.IsAssignableFrom(x)
                                                            && $"I{x.Name}" == repoType.Name
                                                            && !x.IsAbstract);

        var repo = Activator.CreateInstance(implementation, _context);

        if (repo is null)
            throw new Exception(string.Format("No repository found for {0}", nameof(type)));

        _repositories.Add(type, repo);
        return _repositories[type];
    }

    public void Rollback()
    {
        if (_context.Database.CurrentTransaction != null)
            _context.Database.RollbackTransaction();
        Dispose();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (isDisposed) return;

        if (disposing)
        {
            // free managed resources
            _context.Dispose();
        }

        isDisposed = true;
    }
}
