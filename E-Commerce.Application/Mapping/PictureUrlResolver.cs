using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.OrderAggregate;
using Microsoft.Extensions.Configuration;

namespace E_Commerce.Application.Mapping;

public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>, IValueResolver<OrderItem, OrderItemDto, string>
{
    private readonly IConfiguration _configuration;

    public PictureUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
    {
        return BuildUrl(source.PictureUrl);
    }

    public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
    {
        return BuildUrl(source.ItemOrdered.PictureUrl);
    }

    private string BuildUrl(string pictureUrl)
    {
        if (string.IsNullOrWhiteSpace(pictureUrl))
        {
            return string.Empty;
        }

        return $"{_configuration["ApiBaseUrl"]}/{pictureUrl}";
    }
}
