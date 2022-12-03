using Banks.Entities;

namespace Banks.Notifications;

public class ConsoleNotificationStrategy : INotificationStrategy
{
    public void Notify(Client client, string message)
    {
        Console.WriteLine($"Client {client.Name} {client.Surname} get console notification:\n");
        Console.WriteLine(message);
    }
}