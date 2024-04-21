using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Torc.BookLibrary.Domain.Entities;

namespace Repository.Mapping;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");
        builder.HasKey(x => x.Id).HasName("category_id");
        
        builder.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar(50)").IsRequired();
        
        builder.HasQueryFilter(w => w.Active);

        builder.HasMany(x => x.Books).WithOne(x => x.Category);

        builder.HasData(new Category(1, "Sci-Fi"));
        builder.HasData(new Category(2, "Fiction"));
        builder.HasData(new Category(3, "Non-Fiction"));
        builder.HasData(new Category(4, "Biography"));
        builder.HasData(new Category(5, "Mystery"));
    }
}