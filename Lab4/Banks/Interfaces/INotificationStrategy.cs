using Banks.Models;

namespace Banks.Interfaces;

public interface INotificationStrategy
{
    void Notify(IAccountTerms debitAccountTerms);
}