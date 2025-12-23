using Entity;

namespace Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(int? Product_Id, string? name, float? price, int? Catogery_Id, string? description);
    }
}