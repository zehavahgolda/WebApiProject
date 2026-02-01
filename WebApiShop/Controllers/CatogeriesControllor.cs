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
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> Get()
        {
            List<CategoryDto> categories = await _categoryService.GetCategories();
            if (categories == null || categories.Count() == 0)
                return NoContent();
            return Ok(categories);
        }
    }
}
