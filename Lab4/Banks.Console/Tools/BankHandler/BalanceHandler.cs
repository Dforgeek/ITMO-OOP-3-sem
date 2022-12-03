using Banks.ValueObjects;

namespace Banks.Console.Tools.BankHandler;

public class BalanceHandler : IChainOfResponsibility
{
    public IChainOfResponsibility? NextChainElement { get; set; }

    public PosOnlyMoney? Balance { get; private set; }

    public void Handle()
    {
        System.Console.WriteLine("Choose balance for account: ");
        string? command = System.Console.ReadLine();
        if (int.TryParse(command, out _))
            Balance = new PosOnlyMoney(int.Parse(command));
        else
            Handle();
    }
}