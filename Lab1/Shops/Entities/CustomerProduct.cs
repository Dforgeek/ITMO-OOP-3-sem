namespace Shops.Entities;

public class CustomerProduct : Product
{
    public CustomerProduct(Product product, uint amount)
        : base(product.Name, product.Id)
    {
        Quantity = amount;
    }

    public uint Quantity { get; private set; }

    public void IncreaseQuantity(uint increaseAmount)
    {
        Quantity += increaseAmount;
    }
}