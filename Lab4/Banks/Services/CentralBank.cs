using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Services;

public class CentralBank
{
    private readonly List<Client> _clients;
    private readonly List<Bank> _banks;

    public CentralBank()
    {
        _banks = new List<Bank>();
        _clients = new List<Client>();
    }

    public Bank Createbank(string name, PosNegMoney transferLimit)
    {
        return new Bank(name, transferLimit);
    }
}