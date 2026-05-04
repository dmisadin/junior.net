using AbySalto.Junior.Application.Dtos;

namespace AbySalto.Junior.Application.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<int> CreateAsync(CreateProductDto dto);
}