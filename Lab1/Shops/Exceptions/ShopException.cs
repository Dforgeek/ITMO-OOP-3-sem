using Shops.Entities;
using Shops.Models;

namespace Shops.Exceptions;

public class ShopException : Exception
{
    private ShopException(string message)
        : base(message)
    {
    }

    public static ShopException NameIsNullOrEmpty()
        => new ShopException("Name of shop is null or empty");

    public static ShopException AddressIsNullOrEmpty()
        => new ShopException("Adress of shop is null or empty");

    public static ShopException ProductAlreadyExists(Guid id)
        => new ShopException($"The product (ID: {id}) is already in the shop's product list");

    public static ShopException NotEnoughMoneyToPay(Money price, Money offer)
        => new ShopException($"Price is {price}, money offered {offer}");

    public static ShopException NoSuchProduct()
        => new ShopException("No such product in shop");
    public static ShopException NotEnoughProductQuantity()
        => new ShopException("Not enough product in shop");
}