using Domain.Enums;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Provisorio.API.Controllers.Base;

namespace Provisorio.API.Controllers;

public class BookController : BaseController
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("GetAllBySearchType")]
    public async Task<IActionResult> GetAllAsync([FromQuery] SearchType searchBy, 
        [FromQuery] string? value, 
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 15)
    {
        return Ok(await _bookService.GetBySearchTypePagedAsync(searchBy, value, page, pageSize));
    }
}
