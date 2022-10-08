namespace Shops.Exceptions;

public class CustomerException : Exception
{
    private CustomerException() { }

    private CustomerException(string message)
        : base(message)
    {
    }

    public static CustomerException IsNull()
        => new CustomerException("Name consists of zero characters");
}