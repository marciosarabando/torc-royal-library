using Microsoft.EntityFrameworkCore;

namespace Repository.Data;
public class BookLibraryContext : DbContext
{
    public BookLibraryContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookLibraryContext).Assembly);
    }
}
