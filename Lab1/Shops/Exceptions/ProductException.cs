using Shops.Entities;

namespace Shops.Exceptions;

public class ProductException : Exception
{
    private ProductException(string message)
        : base(message) { }

    public static ProductException IsNull()
        => new ProductException("Name consists of zero characters");

    public static ProductException DecreaseError(uint quantity, uint decreaseAmount)
        => new ProductException(
            $"Quantity {quantity} can't be decreased by {decreaseAmount}, else quantity will be negative");
}