using Domain.Entities;
using Domain.Enums;
using Torc.BookLibrary.Domain.Interfaces.Repositories;

namespace Domain.Interfaces.Repositories;
public interface IBookRepository : IGenericRepository<Book>
{
    Task<List<Book>> GetAllPagedBySearchTypeAsync(SearchType searchType, string value, int page, int pageSize);
}
