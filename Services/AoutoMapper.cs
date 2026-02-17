using AutoMapper;
using Entity;
using DTOs;

namespace Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
         
            CreateMap<Category, CatogeryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User != null ? src.User.FirstName.Trim() : string.Empty))
                .ForMember(dest => dest.UserlastName, opt => opt.MapFrom(src => src.User != null ? src.User.LastName.Trim() : string.Empty))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrdeItems));
            CreateMap<OrdeItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName.Trim() : string.Empty))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product != null ? src.Product.Price : 0));
        }
    }
}