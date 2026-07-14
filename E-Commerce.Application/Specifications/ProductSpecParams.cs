namespace E_Commerce.Application.Specifications;

public class ProductSpecParams
{
    private const int MaxPageSize = 50;
    private int pageSize = 6;
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public string? Sort { get; set; }
    public string? Search { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get => pageSize; set => pageSize = value > MaxPageSize ? MaxPageSize : Math.Max(1, value); }
}
