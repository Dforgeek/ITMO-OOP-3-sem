using System.Security.Cryptography.X509Certificates;

namespace Shops.Exceptions;

public class ShipmentException : Exception
{
    private ShipmentException(string message)
        : base(message)
    {
    }

    public static ShipmentException IsNull()
        => new ShipmentException("Shipment is null");
}