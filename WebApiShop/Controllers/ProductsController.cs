using Microsoft.AspNetCore.Mvc;
using Services;
using Repository;
using Entity;
using System.Threading.Tasks;
using DTOs;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductservice _productservice;

        public ProductsController(IProductservice productservice)
        {
            _productservice = productservice;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get(int? Product_Id, string? name, float? price, int? Catogery_Id, string? description)
        {
            List<ProductDto> products = (List<ProductDto>)await _productservice.GetProducts(Product_Id, name, price, Catogery_Id, description);

            if (products == null || products.Count == 0)
            {
                return NoContent();
            }

            return Ok(products);
        }


    }
}

