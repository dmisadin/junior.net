using AbySalto.Junior.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbySalto.Junior.Infrastructure.Database.DbMaps;

public class CustomerDbMap : DbMapBase<Customer>
{
    protected override string Table => "Customers";

    protected override void Map(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.ContactNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.DeliveryAddress)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}