using AutoMapper;
using Domain.Dtos.Response;
using Domain.Entities;

namespace Torc.BookLibrary.IoC.AutoMapper;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Book, BookResponse>()
            .ForMember(d => d.CategoryName, s => s.MapFrom(o => o.Category.Name));
    }
}
