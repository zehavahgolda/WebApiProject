using AutoMapper;
using DTOs;
using Entity    ;
using Microsoft.EntityFrameworkCore;
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
       
 public async Task<FinalProducts> GetProducts(int[]?categoryId, string? q, double? minPrice, double? maxPrice, string? color, string? material, bool? inStock, bool? isActive, string? sort, int? skip, int? position)
        {
            var (products, total) = await _productRepository.GetProducts(
                categoryId, q, minPrice, maxPrice, color, material,
                inStock, isActive, sort, skip, position);

            var itemsDto = _imapper.Map<List<ProductDto>>(products);

            int pageSize = (skip.HasValue && skip.Value > 0) ? skip.Value : 8;
            int page = (position.HasValue && position.Value > 0) ? position.Value : 1;

            bool hasNext = (total - (page * pageSize)) > 0;
            bool hasPrev = page > 1;

            return new FinalProducts(itemsDto, total, hasNext, hasPrev);
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }


        public async Task<Product> AddProduct(Product product)
        {
            return await _productRepository.AddProduct(product);
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            return await _productRepository.UpdateProduct(id, product);
        }

        public async Task DeleteProduct(int id)
        {
         
            await _productRepository.DeleteProduct(id);
        }

    }
}
