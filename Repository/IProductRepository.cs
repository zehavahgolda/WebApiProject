using Entity;

namespace Repository
{
    public interface IProductRepository
    {
        public Task<(List<Product> Items, int TotalCount)> GetProducts(string? name, int?[] categories, int? minPrice, 
         int? maxPrice, string? description, int? position, int? skip);
    }
}