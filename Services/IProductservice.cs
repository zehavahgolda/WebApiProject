using DTOs;

namespace Services
{
    public interface IProductservice
    {
        public Task<FinalProducts> GetProducts(string? name, int?[] categories, int? minPrice, int? maxPrice,
             string? description, int? position, int? skip);
    }
}