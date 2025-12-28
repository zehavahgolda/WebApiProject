using DTOs;

namespace Services
{
    public interface IProductservice
    {
        public  Task<List<ProductDto>> GetProducts(int? Product_Id, string? name, float? price,
                  int? Catogery_Id, string? description);
    }
}