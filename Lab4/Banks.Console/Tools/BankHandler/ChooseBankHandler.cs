using Banks.BankServices;

namespace Banks.Console.Tools.BankHandler;

public class ChooseBankHandler : IChainOfResponsibility
{
    public ChooseBankHandler(CentralBank centralBank)
    {
        CentralBank = centralBank;
        NextChainElement = null;
    }

    public CentralBank CentralBank { get; }
    public Bank? DesiredBank { get; private set; }
    public IChainOfResponsibility? NextChainElement { get; set; }

    public void Handle()
    {
        System.Console.WriteLine(
            "Choose a desired bank by number:");
        var banks = CentralBank.Banks.ToList();
        for (int i = 0; i < banks.Count; i++)
        {
            System.Console.WriteLine($"{i}) {banks[i].Name}");
        }

        string command = System.Console.ReadLine() ?? throw new Exception();
        DesiredBank = banks[int.Parse(command)];
    }
}