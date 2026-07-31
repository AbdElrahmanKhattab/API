using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.OrderAggregate;

namespace E_Commerce.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(destination => destination.BrandName, options => options.MapFrom(source => source.Brand.Name))
            .ForMember(destination => destination.TypeName, options => options.MapFrom(source => source.Type.Name))
            .ForMember(destination => destination.PictureUrl, options => options.MapFrom<PictureUrlResolver>());

        CreateMap<ProductBrand, BrandDto>();
        CreateMap<ProductType, TypeDto>();
        CreateMap<DeliveryMethod, DeliveryMethodDto>();
        CreateMap<OrderAddress, OrderAddressDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(destination => destination.ProductItemId, options => options.MapFrom(source => source.ItemOrdered.ProductItemId))
            .ForMember(destination => destination.ProductName, options => options.MapFrom(source => source.ItemOrdered.ProductName))
            .ForMember(destination => destination.PictureUrl, options => options.MapFrom<PictureUrlResolver>());
        CreateMap<Order, OrderDto>()
            .ForMember(destination => destination.DeliveryMethod, options => options.MapFrom(source => source.DeliveryMethod.ShortName))
            .ForMember(destination => destination.DeliveryMethodCost, options => options.MapFrom(source => source.DeliveryMethod.Price))
            .ForMember(destination => destination.Status, options => options.MapFrom(source => source.Status.ToString()));
    }
}
