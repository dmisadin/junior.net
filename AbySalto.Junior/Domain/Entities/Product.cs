namespace AbySalto.Junior.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;


        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // reverse nav prop
    }
}
