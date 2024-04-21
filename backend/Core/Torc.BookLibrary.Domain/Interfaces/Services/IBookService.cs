using Domain.Enums;
using Torc.BookLibrary.Domain.Dtos.Response;

namespace Domain.Interfaces.Services;
public interface IBookService
{
    Task<GenericResponsePaged> GetBySearchTypePagedAsync(SearchType searchBy, string value, int page, int pageSize);
}
