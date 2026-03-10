using DTOs;
using Entity;

namespace Services
{
    public interface IProductservice
    {
        Task<FinalProducts> GetProducts(int[]? categoryId, string? q, double? minPrice, double? maxPrice, string? color,
            string? material, bool? inStock, bool? isActive, string? sort, int? skip, int? position);
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(int id, Product product);
        public Task DeleteProduct(int id);



    }
}