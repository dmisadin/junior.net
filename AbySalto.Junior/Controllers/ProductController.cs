using AbySalto.Junior.Application.Dtos;
using AbySalto.Junior.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ProductDto>> GetAll()
    {
        var products = await this.productService.GetAllAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductDto dto)
    {
        var id = await this.productService.CreateAsync(dto);
        return Ok(id);
    }
}