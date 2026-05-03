namespace AbySalto.Junior.Domain.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
