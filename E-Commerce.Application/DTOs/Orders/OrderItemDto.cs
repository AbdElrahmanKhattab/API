namespace E_Commerce.Application.DTOs.Orders;

public class OrderItemDto
{
    public int ProductItemId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
