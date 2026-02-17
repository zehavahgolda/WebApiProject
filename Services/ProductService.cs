using AutoMapper;
using DTOs;
using Entity    ;
using Repository;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class Productservice : IProductservice

    {
        IProductRepository _productRepository;
        IMapper _imapper;

        public Productservice(IProductRepository productRepository, IMapper imapper)
        {
            _productRepository = productRepository;
            _imapper = imapper;
        }
        public async Task<FinalProducts> GetProducts(string? name, int?[] categories, int? minPrice, int? maxPrice,
            string? description, int? position, int? skip)
        {
            int pageSize = skip ?? 8;
            int currentPage = position ?? 1;
            var result = await _productRepository.GetProducts(name, categories, minPrice, maxPrice, description, position, skip);
            bool hasNext = (currentPage * pageSize) < result.TotalCount;
            bool hasPrev = currentPage > 1;
            return new FinalProducts
            {
                Items = _imapper.Map<List<Product>, List<ProductDto>>(result.Items),
                TotalCount = result.TotalCount,
                HasNext = hasNext,
                HasPrev = hasPrev
            };

        }
        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }



    }
}
