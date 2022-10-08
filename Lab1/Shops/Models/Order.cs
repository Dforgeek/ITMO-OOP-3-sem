using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public class Order
{
    private readonly List<CustomerProduct> _order;

    public Order()
    {
        _order = new List<CustomerProduct>();
    }

    public IReadOnlyCollection<CustomerProduct> CustomerProducts => _order.AsReadOnly();

    public CustomerProduct? FindProduct(Guid productId)
    {
        return _order.FirstOrDefault(product => product.Id == productId);
    }

    public void AddCustomerProduct(CustomerProduct newCustomerProduct)
    {
        CustomerProduct? oldProduct = FindProduct(newCustomerProduct.Id);
        oldProduct?.IncreaseQuantity(newCustomerProduct.Quantity);

        _order.Add(newCustomerProduct);
    }
}