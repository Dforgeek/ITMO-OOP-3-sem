using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;
public class Shipment
{
    private readonly List<ShopProduct> _shipment;

    public Shipment()
    {
        _shipment = new List<ShopProduct>();
    }

    public IReadOnlyCollection<ShopProduct> ShopProducts => _shipment.AsReadOnly();

    public ShopProduct? FindProduct(Guid shopProductId)
    {
        return _shipment.FirstOrDefault(shopProduct => shopProduct.Id == shopProductId);
    }

    public void AddShopProduct(ShopProduct newShopProduct)
    {
        if (newShopProduct is null)
            throw ProductException.IsNull();
        ShopProduct? oldProduct = FindProduct(newShopProduct.Id);
        if (oldProduct is not null)
        {
            oldProduct.IncreaseQuantity(newShopProduct.Quantity);
            oldProduct.ChangePrice(newShopProduct.Price);
        }

        _shipment.Add(newShopProduct);
    }

    public void AddShopProducts(Shipment newShipment)
    {
        if (newShipment is null)
            throw ShipmentException.IsNull();
        foreach (ShopProduct shopProduct in newShipment._shipment)
        {
            AddShopProduct(new ShopProduct(shopProduct));
        }
    }
}