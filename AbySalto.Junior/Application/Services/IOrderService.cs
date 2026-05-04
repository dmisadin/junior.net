using AbySalto.Junior.Application.Dtos;
using AbySalto.Junior.Domain.Enums;

namespace AbySalto.Junior.Application.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllAsync(OrderSort sort = OrderSort.None);
    Task<OrderDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateOrderDto dto);
    Task<bool> UpdateStatusAsync(int id, UpdateOrderStatusDto dto);
}
