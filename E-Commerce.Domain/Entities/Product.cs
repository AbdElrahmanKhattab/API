namespace E_Commerce.Domain.Entities;

public class Product : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int BrandId { get; set; }
    public int TypeId { get; set; }
    public ProductBrand Brand { get; set; } = null!;
    public ProductType Type { get; set; } = null!;
}
