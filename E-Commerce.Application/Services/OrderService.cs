using AutoMapper;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.OrderAggregate;

namespace E_Commerce.Application.Services;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IBasketRepository basketRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDto?> CreateOrderAsync(string userEmail, CreateOrderDto orderDto)
    {
        var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId);
        if (basket is null || basket.Items.Count == 0)
        {
            return null;
        }

        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(orderDto.DeliveryMethodId);
        if (deliveryMethod is null)
        {
            return null;
        }

        var orderItems = new List<OrderItem>();
        foreach (var item in basket.Items)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
            if (product is null)
            {
                return null;
            }

            var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
            orderItems.Add(new OrderItem(productItemOrdered, product.Price, item.Quantity));
        }

        var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
        var address = _mapper.Map<OrderAddress>(orderDto.ShippingAddress);
        var order = new Order(userEmail, address, deliveryMethod, orderItems, subTotal);

        _unitOfWork.Repository<Order>().Add(order);
        var result = await _unitOfWork.CompleteAsync();

        if (result <= 0)
        {
            return null;
        }

        await _basketRepository.DeleteBasketAsync(orderDto.BasketId);

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IReadOnlyList<OrderDto>> GetOrdersForUserAsync(string userEmail)
    {
        var spec = new OrdersWithItemsAndDeliveryMethodSpecification(userEmail);
        var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
        return _mapper.Map<IReadOnlyList<OrderDto>>(orders);
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int orderId, string userEmail)
    {
        var spec = new OrdersWithItemsAndDeliveryMethodSpecification(orderId, userEmail);
        var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);
        return order is null ? null : _mapper.Map<OrderDto>(order);
    }

    public async Task<IReadOnlyList<DeliveryMethodDto>> GetDeliveryMethodsAsync()
    {
        var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        return _mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethods);
    }
}
