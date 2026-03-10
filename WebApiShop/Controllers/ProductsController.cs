using AutoMapper;
using DTOs;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductservice _productservice;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    Store_329391924Context _store_329391924Context;
  

    public ProductsController(IProductservice productservice, IMapper mapper, IWebHostEnvironment env)
    {
        _productservice = productservice;
        _mapper = mapper;
        _env = env;
    }
   


    [HttpGet]
    public async Task<ActionResult<FinalProducts>> GetProducts(
        [FromQuery] int[]? categoryId,
        [FromQuery] string? q,
        [FromQuery] double? minPrice,
        [FromQuery] double? maxPrice,
        [FromQuery] string? color,
        [FromQuery] string? material,
        [FromQuery] bool? inStock,
        [FromQuery] bool? isActive,
        [FromQuery] string? sort,
        [FromQuery] int? skip,
        [FromQuery] int? position)
    {
        var result = await _productservice.GetProducts(
            categoryId, q, minPrice, maxPrice,
            color, material, inStock, isActive,
            sort, skip, position);

        if (result.Items == null || !result.Items.Any())
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

  

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Post([FromBody] ProductDto productDto)
    {
        var product = _mapper.Map<ProductDto, Product>(productDto);
        var newProduct = await _productservice.AddProduct(product);

        var resultDto = _mapper.Map<Product, ProductDto>(newProduct);
        return CreatedAtAction(nameof(GetById), new { id = resultDto.ProductId }, resultDto);
    }


    [HttpPut("{id}")]
  
    public async Task<IActionResult> Put(int id, [FromBody] ProductDto productDto)
    {
        var productToUpdate = _mapper.Map<ProductDto, Product>(productDto);
        var updatedProduct = await _productservice.UpdateProduct(id, productToUpdate);

        if (updatedProduct == null) return NotFound();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        
        var product = await _productservice.GetProductById(id);
        if (product == null)
        {
            return NotFound(new { message = "המוצר לא נמצא במערכת" });
        }

        try
        {
            await _productservice.DeleteProduct(id);
            return NoContent();
       }
        catch (Exception ex)
        {
            return BadRequest(new { message = "לא ניתן למחוק מוצר המקושר להזמנות קיימות" });
        }
    }
    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        var uploadsFolder = Path.Combine(_env.WebRootPath, "products");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var imageUrl = $"products/{uniqueFileName}";
        return Ok(new { imageUrl });
    }

}