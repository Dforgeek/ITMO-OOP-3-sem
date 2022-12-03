using Banks.Entities;

namespace Banks.Notifications;

public interface INotificationStrategy
{
    void Notify(Client client, string message);
}