using E_Commerce.Domain.Entities.OrderAggregate;
using E_Commerce.Domain.Specifications;

namespace E_Commerce.Application.Specifications;

public class OrdersWithItemsAndDeliveryMethodSpecification : BaseSpecification<Order>
{
    public OrdersWithItemsAndDeliveryMethodSpecification(string userEmail)
        : base(order => order.UserEmail == userEmail)
    {
        AddInclude(order => order.Items);
        AddInclude(order => order.DeliveryMethod);
        AddOrderByDesc(order => order.Id);
    }

    public OrdersWithItemsAndDeliveryMethodSpecification(int id, string userEmail)
        : base(order => order.Id == id && order.UserEmail == userEmail)
    {
        AddInclude(order => order.Items);
        AddInclude(order => order.DeliveryMethod);
    }
}
