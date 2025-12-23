using DTOs;

namespace Services
{
    public interface IProductservice
    {
        Task<IEnumerable<ProductDto>> GetProducts(int? Product_Id, string? name, float? price, int? Catogery_Id, string? description);
    }
}