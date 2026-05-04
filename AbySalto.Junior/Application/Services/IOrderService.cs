using AbySalto.Junior.Application.Dtos;

namespace AbySalto.Junior.Application.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllAsync(bool sortByTotal = false);
    Task<OrderDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateOrderDto dto);
    Task<bool> UpdateStatusAsync(int id, UpdateOrderStatusDto dto);
}
