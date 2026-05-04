using AbySalto.Junior.Application.Dtos;
using AbySalto.Junior.Domain.Entities;
using AbySalto.Junior.Domain.Enums;
using AbySalto.Junior.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AbySalto.Junior.Application.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext context;

    public OrderService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync(OrderSort sort = OrderSort.None)
    {
        var query = context.Orders
            .AsNoTracking()
            .Select(MapToDto());

        query = ApplySorting(query, sort);

        return await query.ToListAsync();
    }
    {
        var query = context.Orders.Select(MapToDto());

        if (sortByTotal)
            query = query.OrderByDescending(o => o.TotalAmount);

        return await query.ToListAsync();
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        return await context.Orders
            .Where(o => o.Id == id)
            .Select(MapToDto())
            .FirstOrDefaultAsync();
    }

    public async Task<int> CreateAsync(CreateOrderDto dto)
    {
        var productIds = dto.Items.Select(i => i.ProductId).ToList();
        var products = await ResolveProductsAsync(productIds);

        var order = new Order
        {
            CustomerId = dto.CustomerId,
            PaymentMethod = dto.PaymentMethod,
            Currency = dto.Currency,
            Note = dto.Note,
            Items = dto.Items.Select(i =>
            {
                var product = products.First(p => p.Id == i.ProductId);
                return new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = i.Quantity,
                    UnitPrice = product.Price
                };
            }).ToList()
        };

        this.context.Orders.Add(order);
        await this.context.SaveChangesAsync();

        return order.Id;
    }

    public async Task<bool> UpdateStatusAsync(int id, UpdateOrderStatusDto dto)
    {
        var order = await this.context.Orders.FindAsync(id);

        if (order is null)
            return false;

        order.Status = dto.Status;
        await this.context.SaveChangesAsync();

        return true;
    }



    private static IQueryable<OrderDto> ApplySorting(IQueryable<OrderDto> query, OrderSort sort)
    {
        return sort switch
        {
            OrderSort.TotalAsc => query.OrderBy(o => o.TotalAmount),
            OrderSort.TotalDesc => query.OrderByDescending(o => o.TotalAmount),
            _ => query
        };
    }

    private async Task<List<Product>> ResolveProductsAsync(List<int> productIds)
    {
        var products = await this.context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();

        if (products.Count != productIds.Count)
            throw new KeyNotFoundException("One or more products not found.");

        return products;
    }

    private static Expression<Func<Order, OrderDto>> MapToDto()
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
            }).ToList()
        };
    }
}
