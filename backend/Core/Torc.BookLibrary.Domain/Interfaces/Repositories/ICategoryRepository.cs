using Domain.Entities;
using Torc.BookLibrary.Domain.Entities;
using Torc.BookLibrary.Domain.Interfaces.Repositories;

namespace Domain.Interfaces.Repositories;
public interface ICategoryRepository : IGenericRepository<Category>
{
}
