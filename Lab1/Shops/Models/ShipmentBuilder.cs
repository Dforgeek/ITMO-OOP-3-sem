using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public class ShipmentBuilder
{
    private Shipment _result;

    public ShipmentBuilder()
    {
        Reset();
        if (_result is null)
            throw ShipmentException.IsNull();
    }

    public void Reset()
    {
        _result = new Shipment();
    }

    public void AddShopProduct(ShopProduct shopProduct)
    {
        _result.AddShopProduct(shopProduct);
    }

    public void AddProducts(Shipment shipment)
    {
        _result.AddShopProducts(shipment);
    }

    public Shipment Build()
    {
        Shipment result = _result;
        Reset();
        return result;
    }
}