using System.Net.Http.Headers;
using Banks.BankServices;
using Banks.Console.Tools.ClientHandlers;
using Banks.Entities;

namespace Banks.Console.Tools;

public class ClientHandler : IChainOfResponsibility
{
    public ClientHandler(CentralBank centralBank)
    {
        CentralBank = centralBank;
    }

    public CentralBank CentralBank { get; }
    public IChainOfResponsibility? NextChainElement { get; set; }

    public void Handle()
    {
        System.Console.WriteLine("Commands: createClient, printClients, exit");
        string? command = System.Console.ReadLine();
        if (string.IsNullOrEmpty(command) || command == "exit")
        {
            return;
        }

        switch (command)
        {
            case "createClient":
            {
                var builder = new Client.ClientBuilder();
                var name = new NameHandler(builder);
                var surname = new SurnameHandler(builder);
                var address = new AddressHandler(builder);
                var passport = new PassportHandler(builder);

                name.NextChainElement = surname;
                surname.NextChainElement = address;
                address.NextChainElement = passport;

                name.Handle();

                builder.SetId(Guid.NewGuid());
                Client client = builder.Build();
                CentralBank.AddClient(client);
                break;
            }

            case "printClients":
            {
                System.Console.WriteLine("Central bank clients:");
                var clients = CentralBank.Clients.ToList();
                for (int i = 0; i < clients.Count; i++)
                {
                    System.Console.WriteLine($"{i}) {clients[i].Name} {clients[i].Surname}");
                }

                break;
            }
        }

        if (NextChainElement != null)
            NextChainElement.Handle();
        else
            Handle();
    }
}