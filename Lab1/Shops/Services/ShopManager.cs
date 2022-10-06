using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class ShopManager
{
    private readonly List<Shop> _shops;

    public ShopManager()
    {
        _shops = new List<Shop>();
    }

    public Shop RegisterShop(string name, string address)
    {
        var newShop = new Shop(Guid.NewGuid(), name, address);
        _shops.Add(newShop);
        return newShop;
    }

    public Shop? FindShop(Guid shopId)
    {
        return _shops.FirstOrDefault(shop => shop.Id == shopId);
    }

    public List<Shop> ShopsToOrder(Order order)
    {
        var result = _shops.Where(shop => shop.HasEnoughProducts(order)).ToList();
        return result;
    }

    public Shop CheapestShopToOrder(Order order)
    {
        List<Shop> shops = ShopsToOrder(order);
        if (shops.Count == 0)
            throw ShopException.NotEnoughProductQuantity(); // TODO: make Shop Manager Exceptions
        Shop result = shops[0];
        shops.RemoveAt(0);
        foreach (Shop shop in shops
                     .Where(shop => shop.GetSumOfOrder(order).Value > result.GetSumOfOrder(order).Value))
        {
            result = shop;
        }

        return result;
    }
}