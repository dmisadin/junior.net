using AbySalto.Junior.Domain.Enums;

namespace AbySalto.Junior.Application.Dtos;

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public Currency Currency { get; set; }
    public string? Note { get; set; }
    public List<CreateOrderItemDto> Items { get; set; } = new();
}

public class CreateOrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}