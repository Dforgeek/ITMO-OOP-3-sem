using System.Net.Mime;

namespace Banks.Interfaces;

public interface IBank
{
    void Transfer(IBankAccount fromAccount, IBankAccount toAccount, decimal amount);
}