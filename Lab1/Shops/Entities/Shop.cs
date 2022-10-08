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

    public void ChangePrice(Guid productId, Money newPrice)
    {
        GetProduct(productId).ChangePrice(newPrice);
    }

    public void ShipmentDelivery(Shipment shipment)
    {
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
        decimal sum = order
            .CustomerProducts.Sum(product => GetProduct(product.Id).Price.Value * product.Quantity);

        return new Money(sum);
    }

    public void Buy(Customer customer, Order order)
    {
        if (!HasEnoughProducts(order))
            throw ShopException.NotEnoughProductQuantity();
        Money priceOfOrder = GetSumOfOrder(order);
        if (priceOfOrder.Value > customer.Money.Value)
            throw ShopException.NotEnoughMoneyToPay(priceOfOrder, customer.Money);
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