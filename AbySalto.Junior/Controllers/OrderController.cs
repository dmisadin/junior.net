using AbySalto.Junior.Application.Dtos;
using AbySalto.Junior.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService orderService;

    public OrderController(IOrderService orderService)
    {
        this.orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAll([FromQuery] bool sortByTotal = false)
    {
        var orders = await this.orderService.GetAllAsync(sortByTotal);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var order = await this.orderService.GetByIdAsync(id);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateOrderDto dto)
    {
        var id = await this.orderService.CreateAsync(dto);
        return Ok(id);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDto dto)
    {
        var updated = await this.orderService.UpdateStatusAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }
}
