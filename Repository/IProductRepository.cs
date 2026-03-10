using Entity;

namespace Repository
{
    public interface IProductRepository
    {
        
        Task<(IEnumerable<Product> products, int total)> GetProducts(int[]? categoryId, string? q, double? minPrice, 
            double? maxPrice, string? color, string? material, bool? inStock, bool? isActive, string? sort, int? skip, int? position);
        public Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(int id, Product product);
        public Task DeleteProduct(int id);

    }
}

