using Banks.ValueObjects;

namespace Banks.BankAccountTerms;

public interface IBankAccountTerms
{
    public PosOnlyMoney UnreliableClientLimit { get; }
}