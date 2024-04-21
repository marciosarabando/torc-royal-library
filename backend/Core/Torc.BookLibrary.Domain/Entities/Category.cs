using Domain.Entities;
using Domain.Entities.Base;

namespace Torc.BookLibrary.Domain.Entities;
public class Category : BaseEntity
{
    public Category() { }

    public Category(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public string Name { get; set; }
    public IEnumerable<Book> Books { get; set; }
}
