using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(destination => destination.ProductBrand, options => options.MapFrom(source => source.Brand.Name))
            .ForMember(destination => destination.ProductType, options => options.MapFrom(source => source.Type.Name))
            .ForMember(destination => destination.PictureUrl, options => options.MapFrom<PictureUrlResolver>());
        CreateMap<ProductBrand, BrandDto>();
        CreateMap<ProductType, TypeDto>();
    }
}
