using AutoMapper;
using Domain.Entities.Base;
using Domain.Interfaces.Services.Base;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Torc.BookLibrary.Domain.Interfaces.Repositories;

namespace Torc.Application.Base;

public abstract class BaseApplicationService<TService, TRepository> : BaseService, IBaseApplicationService<TService> where TService : BaseEntity where TRepository : IGenericRepository<TService>
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly TRepository _repository;
    protected readonly ILogger<BaseApplicationService<TService, TRepository>> _logger;

    public BaseApplicationService(IUnitOfWork unitOfWork, ILogger<BaseApplicationService<TService, TRepository>> logger, IMapper mapper) : base(logger, mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = (TRepository)_unitOfWork.GetRepository<TService, TRepository>();
        _logger = logger;
    }

    public async Task<TService> FindOne(Expression<Func<TService, bool>> predicate)
    {
        return await _repository.FirstAsync(predicate);
    }

    public async Task Insert(TService entity)
    {
        _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Update(TService entity)
    {
        _repository.Update(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}
