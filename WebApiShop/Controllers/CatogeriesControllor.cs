using DTOs;
using DTOs.DTOs;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;
using System.Threading.Tasks;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICatgoryService _categoryService;

        public CategoriesController(ICatgoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CatogeryDto>>> Get()
        {
            var categories = await _categoryService.GetCatogries();

            if (categories == null || !categories.Any())
            {
                return NoContent();
            }

            return Ok(categories);
        }



    }
}
