using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using AutoMapper;
using Entity;

namespace Services
{
    public class AoutoMapper:Profile
    {
        public AoutoMapper()
        {
            CreateMap<Category, CatogeryDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }

}
