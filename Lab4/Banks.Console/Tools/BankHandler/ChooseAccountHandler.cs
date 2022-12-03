using Banks.BankAccounts;
using Banks.BankServices;

namespace Banks.Console.Tools.BankHandler;

public class ChooseAccountHandler : IChainOfResponsibility
{
    public ChooseAccountHandler(CentralBank cb)
    {
        CentralBank = cb;
    }

    public CentralBank CentralBank { get; }
    public Bank? Bank { get; private set; }
    public IBankAccount? BankAccount { get; private set; }
    public IChainOfResponsibility? NextChainElement { get; set; }

    public void Handle()
    {
        var chooseBank = new ChooseBankHandler(CentralBank);
        chooseBank.Handle();
        Bank = chooseBank.DesiredBank ?? throw new Exception();

        System.Console.WriteLine(
            "Choose a desired bank by number:");
        var accounts = Bank.BankAccounts.ToList();
        for (int i = 0; i < accounts.Count; i++)
        {
            System.Console.WriteLine($"{i}) {accounts[i].Id} {accounts[i].Client.Surname}");
        }

        string? command = System.Console.ReadLine();
        if (int.TryParse(command, out _))
        {
            if (int.Parse(command) >= 0 && int.Parse(command) < accounts.Count)
            {
                BankAccount = accounts[int.Parse(command)];
                return;
            }
        }

        if (NextChainElement != null)
            NextChainElement.Handle();
        else
            Handle();
    }
}