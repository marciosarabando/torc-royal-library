using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Base;
using Torc.BookLibrary.Domain.Entities;

namespace Repository.Repositories;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(BookLibraryContext context) : base(context)
    {
    }

    //public async Task<int> GetTotalLoansByMemberNameAndIntervalAsync(string name, int totalDaysInterval)
    //{
    //    return await Get()
    //           .Where(x => x.Member.Name == name
    //             && x.LoanDate.Date > DateTime.Now.AddDays(-totalDaysInterval).Date)
    //           .CountAsync();
    //}
}
