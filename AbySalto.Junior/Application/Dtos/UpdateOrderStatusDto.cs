using AbySalto.Junior.Domain.Enums;

namespace AbySalto.Junior.Application.Dtos;

public class UpdateOrderStatusDto
{
    public OrderStatus Status { get; set; }
}