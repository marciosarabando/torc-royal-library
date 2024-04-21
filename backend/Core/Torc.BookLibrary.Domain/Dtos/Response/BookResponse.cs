using Torc.BookLibrary.Domain.Entities;

namespace Domain.Dtos.Response;
public class BookResponse
{
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int TotalCopies { get; set; }
    public int CopiesInUse { get; set; }
    public string Type { get; set; }
    public string Isbn { get; set; }
    public bool Love { get; set; }
    public string CategoryName { get; set; }
}
