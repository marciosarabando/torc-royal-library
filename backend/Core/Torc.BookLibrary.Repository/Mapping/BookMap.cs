using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping;

public class BookMap : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Book");
        builder.HasKey(x => x.Id).HasName("book_id");

        builder.Property(x => x.Title).HasColumnName("title").HasColumnType("varchar(100)").IsRequired();
        builder.Property(x => x.FirstName).HasColumnName("first_name").HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.LastName).HasColumnName("last_name").HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.TotalCopies).HasColumnName("total_copies").HasColumnType("int").IsRequired();
        builder.Property(x => x.CopiesInUse).HasColumnName("copies_in_use").HasColumnType("int").IsRequired();
        builder.Property(x => x.Type).HasColumnName("type").HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.Isbn).HasColumnName("isbn").HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.Love).HasColumnName("love").HasColumnType("bit").IsRequired();
        builder.Property(x => x.CategoryId).HasColumnName("categoryId").HasColumnType("int").IsRequired();

        builder.HasOne(x => x.Category).WithMany(x => x.Books);
        
        builder.HasQueryFilter(w => w.Active);

        builder.HasData(new Book(1, "Pride and Prejudice", "Jane", "Austen", 100, 80, "Hardcover", "1234567891", true, 2));
        builder.HasData(new Book(2, "To Kill a Mockingbird", "Harper", "Lee", 75, 65, "Paperback", "1234567892", true, 2));
        builder.HasData(new Book(3, "The Catcher in the Rye", "J.D.", "Salinger", 50, 45, "Hardcover", "1234567893", true, 2));
        builder.HasData(new Book(4, "The Great Gatsby", "F. Scott", "Fitzgerald", 50, 22, "Hardcover", "1234567894", false, 3));
        builder.HasData(new Book(5, "The Alchemist", "Paulo", "Coelho", 50, 35, "Hardcover", "1234567895", false, 4));
        builder.HasData(new Book(6, "The Book Thief", "Markus", "Zusak", 75, 11, "Hardcover", "1234567896", false, 5));
        builder.HasData(new Book(7, "The Chronicles of Narnia", "C.S.", "Lewis", 100, 14, "Paperback", "1234567897", true, 1));
        builder.HasData(new Book(8, "The Da Vinci Code", "Dan", "Brown", 50, 40, "Paperback", "1234567898", true, 1));
        builder.HasData(new Book(9, "The Grapes of Wrath", "John", "Steinbeck", 50, 35, "Hardcover", "1234567899", false, 2));
        builder.HasData(new Book(10, "The Hitchhiker's Guide to the Galaxy", "Douglas", "Adams", 50, 35, "Paperback", "1234567900", false, 3));
        builder.HasData(new Book(11, "Moby-Dick", "Herman", "Melville", 30, 8, "Hardcover", "8901234567", false, 2));
        builder.HasData(new Book(12, "To Kill a Mockingbird", "Harper", "Lee", 20, 0, "Paperback", "9012345678", false, 3));
        builder.HasData(new Book(13, "The Catcher in the Rye", "J.D.", "Salinger", 10, 1, "Hardcover", "0123456789", false, 3));
    }
}