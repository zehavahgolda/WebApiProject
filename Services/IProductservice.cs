using Entity;

namespace Services
{
    public interface IProductservice
    {
        Task<List<Product>> GetProducts(int? Product_Id, string? name, float? price, int? Catogery_Id, string? description);
    }
}