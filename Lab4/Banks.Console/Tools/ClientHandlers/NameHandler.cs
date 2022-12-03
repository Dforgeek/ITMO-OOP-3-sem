using Banks.Entities;

namespace Banks.Console.Tools.ClientHandlers;

public class NameHandler : IChainOfResponsibility
{
    private Client.ClientBuilder _builder;

    public NameHandler(Client.ClientBuilder builder)
    {
        _builder = builder;
        NextChainElement = null;
    }

    public IChainOfResponsibility? NextChainElement { get; set; }

    public void Handle()
    {
        System.Console.WriteLine("Set name for client: ");
        string? name = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Handle();
        }
        else
        {
            _builder.SetName(name);
            NextChainElement?.Handle();
        }
    }
}