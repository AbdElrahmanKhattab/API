namespace E_Commerce.Domain.Entities.OrderAggregate;

public class Order : BaseEntity
{
    public Order()
    {
    }

    public Order(string userEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> items, decimal subTotal)
    {
        UserEmail = userEmail;
        ShippingAddress = shippingAddress;
        DeliveryMethod = deliveryMethod;
        Items = items.ToList();
        SubTotal = subTotal;
    }

    public string UserEmail { get; set; } = string.Empty;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public OrderAddress ShippingAddress { get; set; } = null!;
    public DeliveryMethod DeliveryMethod { get; set; } = null!;
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public decimal SubTotal { get; set; }
    public decimal Total => SubTotal + DeliveryMethod.Price;
}
