using E_Commerce.Application.DTOs.Orders;

namespace E_Commerce.Application.Services;

public interface IOrderService
{
    Task<OrderDto?> CreateOrderAsync(string userEmail, CreateOrderDto orderDto);
    Task<IReadOnlyList<OrderDto>> GetOrdersForUserAsync(string userEmail);
    Task<OrderDto?> GetOrderByIdAsync(int orderId, string userEmail);
    Task<IReadOnlyList<DeliveryMethodDto>> GetDeliveryMethodsAsync();
}
