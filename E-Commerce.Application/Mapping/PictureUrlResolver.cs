using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace E_Commerce.Application.Mapping;

public class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductDto, string>
{
    public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrWhiteSpace(source.PictureUrl)) return string.Empty;
        var baseUrl = configuration["ApiBaseUrl"]?.TrimEnd('/');
        return string.IsNullOrWhiteSpace(baseUrl) ? source.PictureUrl : $"{baseUrl}/{source.PictureUrl.TrimStart('/')}";
    }
}
