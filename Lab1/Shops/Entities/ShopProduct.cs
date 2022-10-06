using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class ShopProduct : Product
{
    public ShopProduct(Product product, Money price, uint quantity = 1)
        : base(product.Name, product.Id)
    {
        Price = price;
        Quantity = quantity;
    }

    public ShopProduct(ShopProduct newShopProduct)
        : base(newShopProduct.Name, newShopProduct.Id)
    {
        Price = newShopProduct.Price;
        Quantity = newShopProduct.Quantity;
    }

    public Money Price { get; private set; }
    public uint Quantity { get; private set; }

    public void ChangePrice(Money newPrice)
    {
        Price = newPrice;
    }

    public void IncreaseQuantity(uint increaseAmount)
    {
        Quantity += increaseAmount;
    }

    public void DecreaseQuantity(uint decreaseAmount)
    {
        if (decreaseAmount > Quantity)
            throw ProductException.DecreaseError(Quantity, decreaseAmount);
        Quantity -= decreaseAmount;
    }
}