using AutoMapper;
using DTOs;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Services;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductservice _productservice;
    private readonly IMapper _mapper;

    // הוספתי את ה-IMapper כפרמטר בתוך הסוגריים של הבנאי
    public ProductsController(IProductservice productservice, IMapper mapper)
    {
        _productservice = productservice;
        _mapper = mapper; // השורה הזו היא הקריטית! היא מאתחלת את המפר
    }

    [HttpGet]
    public async Task<ActionResult<FinalProducts>> Get([FromQuery] string? name, [FromQuery] int?[] categories,
    [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] string? description, [FromQuery] int position = 1,
    [FromQuery] int skip = 8)
    {
        FinalProducts result = await _productservice.GetProducts(name, categories, minPrice, maxPrice, description, position, skip);

        if (result == null || result.Items.Count == 0)
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _productservice.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }
        var productDto = _mapper.Map<Product, ProductDto>(product);
        return Ok(productDto);
    }
}