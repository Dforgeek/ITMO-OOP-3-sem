using Shops.Exceptions;

namespace Shops.Entities;

public class Product
{
    public Product(string name, Guid id)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        if (string.IsNullOrEmpty(name))
            throw ProductException.IsNull();
        Name = name;
    }

    public Guid Id { get; }
    public string Name { get; }
}