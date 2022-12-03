using Banks.BankServices;

namespace Banks.Console.Tools;

public class TimeHandler : IChainOfResponsibility
{
    public TimeHandler(CentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    public CentralBank CentralBank { get; }
    public IChainOfResponsibility? NextChainElement { get; set; }

    public void Handle()
    {
        System.Console.WriteLine("Commands: PassOneDay, PassOneMonth, exit");
        string? command = System.Console.ReadLine();
        switch (command)
        {
            case "exit":
                return;
            case "PassOneDay":
                CentralBank.PassOneDay();
                break;
            case "PassOneMonth":
                CentralBank.PassOneMonth();
                break;
        }

        if (NextChainElement != null)
            NextChainElement.Handle();
        else
            Handle();
    }
}