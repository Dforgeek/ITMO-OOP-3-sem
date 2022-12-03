using Banks.BankServices;
using Banks.Entities;

namespace Banks.Console.Tools.BankHandler;

public class ChooseClientHandler : IChainOfResponsibility
{
    public ChooseClientHandler(CentralBank centralBank)
    {
        CentralBank = centralBank;
        NextChainElement = null;
    }

    public CentralBank CentralBank { get; }
    public Client? DesiredClient { get; private set; }
    public IChainOfResponsibility? NextChainElement { get; set; }

    public void Handle()
    {
        System.Console.WriteLine(
            "Choose a desired client by number:");
        var clients = CentralBank.Clients.ToList();
        for (int i = 0; i < clients.Count; i++)
        {
            System.Console.WriteLine($"{i}) {clients[i].Name} {clients[i].Surname}");
        }

        string? command = System.Console.ReadLine();
        if (int.TryParse(command, out _))
        {
            if (int.Parse(command) >= 0 && int.Parse(command) < clients.Count)
            {
                DesiredClient = clients[int.Parse(command)];
                return;
            }
        }

        if (NextChainElement != null)
            NextChainElement.Handle();
        else
            Handle();
    }
}