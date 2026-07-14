namespace E_Commerce.Domain.Entities;

public class ProductBrand : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
