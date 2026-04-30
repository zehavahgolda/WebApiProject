using AutoMapper;
using DTOs;
using Entity;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed; 
using System.Text.Json; 

namespace Services
{
    public class Productservice : IProductservice
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _imapper;
        private readonly IDistributedCache _cache; 

        public Productservice(IProductRepository productRepository, IMapper imapper, IDistributedCache cache)
        {
            _productRepository = productRepository;
            _imapper = imapper;
            _cache = cache;
        }

        public async Task<FinalProducts> GetProducts(int[]? categoryId, string? q, double? minPrice, double? maxPrice, string? color, string? material, bool? inStock, bool? isActive, string? sort, int? skip, int? position)
        {
            string cacheKey = $"products_{q}_{minPrice}_{maxPrice}_{color}_{material}_{sort}_{skip}_{position}";

            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<FinalProducts>(cachedData);
            }

            var (products, total) = await _productRepository.GetProducts(
                categoryId, q, minPrice, maxPrice, color, material,
                inStock, isActive, sort, skip, position);

            var itemsDto = _imapper.Map<List<ProductDto>>(products);

            int pageSize = (skip.HasValue && skip.Value > 0) ? skip.Value : 8;
            int page = (position.HasValue && position.Value > 0) ? position.Value : 1;

            bool hasNext = (total - (page * pageSize)) > 0;
            bool hasPrev = page > 1;

            var finalResult = new FinalProducts(itemsDto, total, hasNext, hasPrev);

            
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };

            var serializedData = JsonSerializer.Serialize(finalResult);
            await _cache.SetStringAsync(cacheKey, serializedData, cacheOptions);

            return finalResult;
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