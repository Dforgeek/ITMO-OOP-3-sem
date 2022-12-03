using Banks.Entities;

namespace Banks.Console.Tools.ClientHandlers;

public class AddressHandler : IChainOfResponsibility
{
    private Client.ClientBuilder _builder;

    public AddressHandler(Client.ClientBuilder builder)
    {
        _builder = builder;
        NextChainElement = null;
    }

    public IChainOfResponsibility? NextChainElement { get; set; }

    public void Handle()
    {
        System.Console.WriteLine("Set address for client (optional): ");
        string? name = System.Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
        {
            _builder.SetAddress(name);
            NextChainElement?.Handle();
        }
        else
        {
            NextChainElement?.Handle();
        }
    }
}