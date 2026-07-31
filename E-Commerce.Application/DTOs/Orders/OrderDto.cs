namespace E_Commerce.Application.DTOs.Orders;

public class OrderDto
{
    public int Id { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public DateTimeOffset OrderDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public OrderAddressDto ShippingAddress { get; set; } = new();
    public string DeliveryMethod { get; set; } = string.Empty;
    public decimal DeliveryMethodCost { get; set; }
    public IReadOnlyList<OrderItemDto> Items { get; set; } = [];
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
}
