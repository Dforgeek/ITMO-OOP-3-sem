using Banks.Models;

namespace Banks.Interfaces;

public interface IBankAccountTerms
{
    public PosOnlyMoney UnreliableClientLimit { get; }
}