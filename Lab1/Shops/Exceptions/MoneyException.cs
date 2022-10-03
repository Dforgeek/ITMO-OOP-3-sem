namespace Shops.Exceptions;

public class MoneyException : Exception
{
    private MoneyException() { }

    private MoneyException(string message)
        : base(message) { }

    public static MoneyException MoneyNumberIsNegativeOrNull(decimal money)
    {
        return new MoneyException($"Money is negative or null {money}");
    }
}