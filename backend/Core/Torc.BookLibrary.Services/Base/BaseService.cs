using AutoMapper;
using Domain.Interfaces.Services.Base;
using Microsoft.Extensions.Logging;
using Torc.BookLibrary.Domain.Dtos.Response;

namespace Torc.Application.Base;
public class BaseService : IBaseService
{
    protected readonly ILogger<BaseService> _logger;
    protected readonly IMapper _mapper;


    public BaseService(ILogger<BaseService> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public GenericResponse GenericResponse(bool status, string mensagem = null, object data = null)
    {
        return new GenericResponse
        {
            Success = status,
            Message = mensagem,
            Data = data
        };
    }
    public GenericResponsePaged GenericResponsePaged(int total, int totalPages, int currentPage, bool status, string mensagem = null, object data = null)
    {
        return new GenericResponsePaged
        {
            Success = status,
            Message = mensagem,
            Data = data,
            Total = total,
            TotalPages = totalPages,
            CurrentPage = currentPage
        };
    }
}
