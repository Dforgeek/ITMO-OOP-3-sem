namespace Shops.Exceptions;

public class ShopException : Exception
{
    private ShopException() { }

    private ShopException(string message)
        : base(message)
    {
    }

    public static ShopException NameIsNullOrEmpty()
    {
        return new ShopException("Name of shop is null or empty");
    }

    public static ShopException AdressIsNullOrEmpty()
    {
        return new ShopException("Adress of shop is null or empty");
    }
}