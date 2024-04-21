using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Base;

namespace Torc.BookLibrary.Repository.Repositories;
public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(BookLibraryContext context) : base(context)
    {
    }

    public async Task<List<Book>> GetAllPagedBySearchTypeAsync(SearchType searchType, string value, int page, int pageSize)
    {
        IQueryable<Book> query = Get();

        switch (searchType)
        {
            case SearchType.AUTHOR:
                query = Get()
                .Include(x => x.Category)
                .Where(x => x.FirstName.ToLower().Contains(value.ToLower()) 
                || x.LastName.ToLower().Contains(value.ToLower()));
                break;
            case SearchType.ISBN:
                query = Get().Include(x => x.Category).Where(x => x.Isbn.Contains(value));
                break;
            case SearchType.OWN_LOVE_WANT_TO_READ:
                query = Get().Include(x => x.Category).Where(x => x.Love);
                break;
            default:
                query = Get().Include(x => x.Category);
                break;
        }

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }
}
