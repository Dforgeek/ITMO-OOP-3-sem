using Banks.Entities;
using Banks.ValueObjects;

namespace Banks.Console.Tools.ClientHandlers;

public class PassportHandler : IChainOfResponsibility
{
    private Client.ClientBuilder _builder;

    public PassportHandler(Client.ClientBuilder builder)
    {
        _builder = builder;
        NextChainElement = null;
    }

    public IChainOfResponsibility? NextChainElement { get; set; }

    public void Handle()
    {
        System.Console.WriteLine("Set passport for client (optional): ");
        string? passportString = System.Console.ReadLine();
        if (passportString != null && Validation(passportString))
        {
            var result = passportString.Split(' ').ToList();
            var passport = new Passport(int.Parse(result[0]), int.Parse(result[1]));
            _builder.SetPassport(passport);
            NextChainElement?.Handle();
        }
    }

    public bool Validation(string passportString)
    {
        var result = passportString.Split(' ').ToList();
        if (result.Count != 2)
            return false;
        return result[0].Length == 4 && result[1].Length == 6
                                     && int.TryParse(result[0], out _) && int.TryParse(result[1], out _);
    }
}