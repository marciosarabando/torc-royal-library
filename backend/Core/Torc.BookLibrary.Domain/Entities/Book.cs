using Domain.Entities.Base;
using Torc.BookLibrary.Domain.Entities;

namespace Domain.Entities;
public class Book : BaseEntity
{
    public Book() { }
    public Book(
        int id,
        string title, 
        string firstName, 
        string lastName, 
        int totalCopies, 
        int copiesInUse, 
        string type, 
        string isbn,
        bool love,
        int categoryId)
    {
        Id = id;
        Title = title;
        FirstName = firstName;
        LastName = lastName;
        TotalCopies = totalCopies;
        CopiesInUse = copiesInUse;
        Type = type;
        Isbn = isbn;
        Love = love;
        CategoryId = categoryId;
    }

    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int TotalCopies { get; set; }
    public int CopiesInUse { get; set; }
    public string Type { get; set; }
    public string Isbn { get; set; }
    public bool Love { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
}
