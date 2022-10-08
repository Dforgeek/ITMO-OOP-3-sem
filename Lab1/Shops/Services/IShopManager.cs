using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopManager
{
    Shop RegisterShop(string name, string address);

    Shop FindShop(Guid shopId);

    List<Shop> ShopsToOrder(Order order);

    Shop CheapestShopToOrder(Order order);
}