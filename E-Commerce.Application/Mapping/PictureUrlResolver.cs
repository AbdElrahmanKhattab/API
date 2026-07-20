using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace E_Commerce.Application.Mapping;

public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
{
    private readonly IConfiguration _configuration;

    public PictureUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrWhiteSpace(source.PictureUrl))
        {
            return string.Empty;
        }

        return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
    }
}
