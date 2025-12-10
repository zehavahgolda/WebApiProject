using Microsoft.AspNetCore.Mvc;
using Services;
using Repository;
using Entity;
using System.Threading.Tasks;

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
        public async Task<ActionResult<List<Product>>> Get(int? Product_Id, string? name, float? price, int? Catogery_Id, string? description)
        {
            return await _productservice.GetProducts(Product_Id, name, price, Catogery_Id, description);
        }
    }
}
