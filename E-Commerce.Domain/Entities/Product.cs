namespace E_Commerce.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
    public int TypeId { get; set; }
    public ProductType Type { get; set; } = null!;
    public int BrandId { get; set; }
    public ProductBrand Brand { get; set; } = null!;
}
