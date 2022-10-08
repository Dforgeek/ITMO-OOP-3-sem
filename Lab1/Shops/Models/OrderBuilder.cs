using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public class OrderBuilder
{
    private Order _result;

    public OrderBuilder()
    {
        Reset();
        if (_result is null)
            throw ShipmentException.IsNull();
    }

    public void Reset()
    {
        _result = new Order();
    }

    public void AddCustomerProduct(CustomerProduct shopProduct)
    {
        _result.AddCustomerProduct(shopProduct);
    }

    public Order Build()
    {
        Order result = _result;
        Reset();
        return result;
    }
}