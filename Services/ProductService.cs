using Repository;
using Entity    ;
using System.Threading.Tasks;

namespace Services
{
    public class Productservice : IProductservice
    {
        IProductRepository _productRepository;

        public Productservice(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetProducts(int? Product_Id, string? name, float? price, int? Catogery_Id, string? description)
        {
            return await _productRepository.GetProducts(Product_Id, name, price, Catogery_Id, description);
        }




    }
}
