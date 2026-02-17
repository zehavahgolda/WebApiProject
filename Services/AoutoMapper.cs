using AutoMapper;
using Entity;  // כדי להכיר את Category, Order וכו'
using DTOs;    // כדי להכיר את CatogeryDto, OrderDto וכו'

namespace Services // המיקום הלוגי הנכון
{
    public class AutoMapperProfile : Profile // תיקנתי גם את שם המחלקה לסטנדרט
    {
        public AutoMapperProfile()
        {
            // שים לב ששמות ה-DTOs חייבים להיות קיימים בפרויקט DTOs
            CreateMap<Category, CatogeryDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}