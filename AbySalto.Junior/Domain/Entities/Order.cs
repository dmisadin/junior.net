using AbySalto.Junior.Domain.Enums;

namespace AbySalto.Junior.Domain.Entities;

public class Order : BaseEntity
{
    public DateTime OrderedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public PaymentMethod PaymentMethod { get; set; }
    public Currency Currency { get; set; } = Currency.EUR;
    public string? Note { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    public decimal TotalAmount => Items.Sum(i => i.LineTotal);
}
