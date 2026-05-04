using AbySalto.Junior.Domain.Entities;
using System.Linq.Expressions;

namespace AbySalto.Junior.Application.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime OrderedAt { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string? Note { get; set; }
    public decimal TotalAmount { get; set; }
    public IEnumerable<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();


    public static Expression<Func<Order, OrderDto>> Projection()
    {
        return o => new OrderDto
        {
            Id = o.Id,
            CustomerName = o.Customer.FirstName + " " + o.Customer.LastName,
            DeliveryAddress = o.Customer.DeliveryAddress,
            ContactNumber = o.Customer.ContactNumber,
            Status = o.Status.ToString(),
            OrderedAt = o.OrderedAt,
            PaymentMethod = o.PaymentMethod.ToString(),
            Currency = o.Currency.ToString(),
            Note = o.Note,

            TotalAmount = o.Items.Sum(i => i.Quantity * i.UnitPrice),

            Items = o.Items.Select(i => new OrderItemDto
            {
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                LineTotal = i.Quantity * i.UnitPrice
            })
        };
    }
}
