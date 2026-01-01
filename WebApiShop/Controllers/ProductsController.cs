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
        public async Task<ActionResult<FinalProducts>> Get([FromQuery] string? name, [FromQuery] int?[] categories,
        [FromQuery] int? minPrice,[FromQuery] int? maxPrice, [FromQuery] string? description, [FromQuery] int position = 1,
    [   FromQuery] int skip = 8)
        {
            FinalProducts result = await _productservice.GetProducts(name, categories, minPrice, maxPrice, description, position, skip);
            if (result == null || result.Items.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }


    }
}

