namespace Shops.Exceptions;

public class ShopProductException : Exception
{
    private ShopProductException(string message)
        : base(message) { }

    public static ShopProductException IsNull()
    {
        return new ShopProductException("Name consists of zero characters");
    }
}