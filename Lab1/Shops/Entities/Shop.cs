using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private readonly List<ShopProduct> _shopProducts;

    public Shop(Guid id, string name, string address)
    {
        if (string.IsNullOrEmpty(name))
            throw ShopException.NameIsNullOrEmpty();
        if (string.IsNullOrEmpty(address))
            throw ShopException.AddressIsNullOrEmpty();
        Name = name;
        Address = address;
        Id = id;
        _shopProducts = new List<ShopProduct>();
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Address { get; }

    public ShopProduct? FindProduct(Guid shopProductId)
    {
        return _shopProducts.FirstOrDefault(shopProduct => shopProduct.Id == shopProductId);
    }

    public ShopProduct GetProduct(Guid shopProductId)
    {
        return FindProduct(shopProductId) ?? throw ShopException.NoSuchProduct();
    }

    public bool HasEnoughProducts(Order order)
    {
        return !order.CustomerProducts
            .Any(customerProduct =>
                customerProduct.Quantity > GetProduct(customerProduct.Id).Quantity);
    }

    public void ShipmentDelivery(Shipment shipment)
    {
        if (shipment is null)
            throw ShipmentException.IsNull();
        foreach (ShopProduct newShopProduct in shipment.ShopProducts)
        {
            ShopProduct? oldProduct = FindProduct(newShopProduct.Id);
            if (oldProduct is null)
            {
                _shopProducts.Add(newShopProduct);
            }
            else
            {
                oldProduct.ChangePrice(newShopProduct.Price);
                oldProduct.IncreaseQuantity(newShopProduct.Quantity);
            }
        }
    }

    public Money GetSumOfOrder(Order order)
    {
        if (order is null)
            throw OrderException.IsNull();
        decimal sum = order.CustomerProducts
            .Select(product => GetProduct(product.Id))
            .Select(shopProduct => shopProduct.Price.Value)
            .Sum();

        return new Money(sum);
    }

    public void Buy(Customer customer, Order order)
    {
        if (customer is null)
            throw CustomerException.IsNull();
        if (order is null)
            throw OrderException.IsNull();
        if (!HasEnoughProducts(order))
            throw ShopException.NotEnoughProductQuantity();
        Money priceOfOrder = GetSumOfOrder(order);
        foreach (CustomerProduct customerProduct in order.CustomerProducts)
            DecreaseAmountOfProduct(customerProduct);

        customer.SpendMoney(priceOfOrder);
    }

    private void DecreaseAmountOfProduct(CustomerProduct customerProduct)
    {
        ShopProduct productToDecrease = GetProduct(customerProduct.Id);
        productToDecrease.DecreaseQuantity(customerProduct.Quantity);
    }
}