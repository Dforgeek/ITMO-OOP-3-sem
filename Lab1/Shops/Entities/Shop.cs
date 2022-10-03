using Shops.Exceptions;

namespace Shops.Entities;

public class Shop
{
    public Shop(string name, string address, Guid id)
    {
        if (string.IsNullOrEmpty(name))
            throw ShopException.NameIsNullOrEmpty();
        if (string.IsNullOrEmpty(address))
            throw ShopException.AdressIsNullOrEmpty();
        Name = name;
        Address = address;
        Id = id;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Address { get; }
}