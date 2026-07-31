namespace E_Commerce.Domain.Entities.OrderAggregate;

public class OrderAddress
{
    public OrderAddress()
    {
    }

    public OrderAddress(string firstName, string lastName, string street, string city, string country)
    {
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        City = city;
        Country = country;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}
