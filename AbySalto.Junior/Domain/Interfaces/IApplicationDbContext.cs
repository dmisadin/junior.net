using AbySalto.Junior.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Domain.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
