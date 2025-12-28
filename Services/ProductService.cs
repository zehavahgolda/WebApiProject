using AutoMapper;
using DTOs;
using Entity    ;
using Repository;
using System.Threading.Tasks;

namespace Services
{
    public class Productservice : IProductservice

    {
        IProductRepository _productRepository;
        IMapper _imapper;

        public Productservice(IProductRepository productRepository, IMapper imapper)
        {
            _productRepository = productRepository;
            _imapper = imapper;
        }

        public async Task<List<ProductDto>> GetProducts(int? Product_Id, string? name, float? price,
           int? Catogery_Id, string? description)
        {
            List<Product> products = await _productRepository.GetProducts(Product_Id, name, price, Catogery_Id, description);
            List<ProductDto> productDtos = _imapper.Map<List<Product>, List<ProductDto>>(products);
            return productDtos;
        }
        



    }
}
