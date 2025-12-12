using Microsoft.AspNetCore.Mvc;
using Services;
using Repository;
using Entity;
using DTOs;
using System.Threading.Tasks;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatogerisController : ControllerBase
    {
        ICatgoryService _catgoryService;

        public CatogerisController(ICatgoryService _catgoryService)
        {
            _catgoryService = _catgoryService;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<List<CatogeryDto>>> Get()
        {
            List<CatogeryDto> categories = await _catgoryService.GetCatogries();
            if (categories == null || categories.Count() == 0)
                return NoContent();
            return Ok(categories);
        }
    }
}
