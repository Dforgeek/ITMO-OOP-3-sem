namespace Shops.Exceptions;

public class OrderException : Exception
{
    private OrderException(string message)
        : base(message)
    {
    }

    public static OrderException IsNull()
    {
        throw new OrderException("Order is null");
    }
}