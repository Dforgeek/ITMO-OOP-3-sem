using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Product
{
    public Product(string name, Price price, uint quantity = 1, Guid id = default)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        if (string.IsNullOrEmpty(name))
            throw new ShopException("Name consists of zero characters");
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public Guid Id { get; }
    public string Name { get; }
    public Price Price { get; private set; }
    public uint Quantity { get; }

    public void ChangePrice(Price newPrice)
    {
        Price = newPrice;
    }
}