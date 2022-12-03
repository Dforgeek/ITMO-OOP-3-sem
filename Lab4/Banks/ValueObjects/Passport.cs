namespace Banks.ValueObjects;

public record Passport
{
    private const int SerialNumberLength = 4;
    private const int PassportCodeLength = 6;
    public Passport(int serialNumber, int passportCode)
    {
        if (serialNumber.ToString().Length > SerialNumberLength)
            throw new Exception();

        if (passportCode.ToString().Length > PassportCodeLength)
            throw new Exception();

        SerialNumber = serialNumber;
        PassportCode = passportCode;
    }

    public int SerialNumber { get; }

    public int PassportCode { get; }
}