using AutoMapper;
using Domain.Dtos.Response;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Torc.Application.Base;
using Torc.BookLibrary.Domain.Dtos.Response;
using Torc.BookLibrary.Domain.Interfaces.Repositories;

namespace Services;

public class BookService : BaseApplicationService<Book, IBookRepository>, IBookService
{
    public BookService(IUnitOfWork unitOfWork,
        ILogger<BookService> logger,
        IMapper mapper) : base(unitOfWork, logger, mapper)
    {
    }

    public async Task<GenericResponsePaged> GetBySearchTypePagedAsync(SearchType searchBy, string value, int page, int pageSize)
    {
        var booksList = await _repository.GetAllPagedBySearchTypeAsync(searchBy, value, page, pageSize);

        var total = booksList.Count();
        var totalPages = (int)Math.Ceiling((double)total / pageSize);

      var response = _mapper.Map<List<BookResponse>>(booksList);

        return GenericResponsePaged(total, totalPages, page, true, null, response);
    }
}