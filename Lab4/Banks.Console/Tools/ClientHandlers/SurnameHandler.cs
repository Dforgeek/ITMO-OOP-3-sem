using Banks.Entities;

namespace Banks.Console.Tools.ClientHandlers;

public class SurnameHandler : IChainOfResponsibility
{
    private Client.ClientBuilder _builder;

    public SurnameHandler(Client.ClientBuilder builder)
    {
        _builder = builder;
        NextChainElement = null;
    }

    public IChainOfResponsibility? NextChainElement { get; set; }

    public void Handle()
    {
        System.Console.WriteLine("Set surname for client: ");
        string? name = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Handle();
        }
        else
        {
            _builder.SetSurname(name);
            NextChainElement?.Handle();
        }
    }
}