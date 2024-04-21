namespace Torc.BookLibrary.Domain.Dtos.Response;
public class GenericResponse
{
    public GenericResponse() { }
    public GenericResponse(bool success, string menssage, object data)
    {
        Success = success;
        Message = menssage;
        Data = data;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
}

public class GenericResponsePaged
{
    public GenericResponsePaged()
    {

    }

    public GenericResponsePaged(bool success, string message, object data, int total, int totalPages, int currentPage)
    {
        Success = success;
        Message = message;
        Data = data;
        Total = total;
        TotalPages = totalPages;
        CurrentPage = currentPage;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }

}