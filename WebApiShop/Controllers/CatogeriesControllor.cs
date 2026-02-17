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



        //[HttpGet("{id}")]
        //public async Task<ActionResult<CatogeryDto>> GetById(int id)
        //{
        //    var category = await _categoryService.GetCategoryById(id);
        //    if (category == null)
        //    {
        //        return NotFound($"Category with ID {id} not found.");
        //    }

        //    return Ok(category);
        //}
    }
}
