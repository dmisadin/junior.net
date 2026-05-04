using AbySalto.Junior.Application.Dtos;
using AbySalto.Junior.Domain.Entities;
using AbySalto.Junior.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Application.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext context;

    public ProductService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        return await this.context.Products
            .Where(p => p.IsAvailable)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            })
            .ToListAsync();
    }

    public async Task<int> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price
        };

        this.context.Products.Add(product);
        await this.context.SaveChangesAsync();

        return product.Id;
    }
}
