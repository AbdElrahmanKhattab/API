namespace E_Commerce.Domain.Entities;

public class CustomerBasket
{
    public CustomerBasket()
    {
    }

    public CustomerBasket(string id)
    {
        Id = id;
    }

    public string Id { get; set; } = string.Empty;
    public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
}
