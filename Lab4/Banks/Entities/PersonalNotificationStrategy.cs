using Banks.Interfaces;

namespace Banks.Entities;

public class SMSNotificationStrategy : INotificationStrategy
{
    public void Notify(IAccountTerms debitAccountTerms)
    {
        Console.WriteLine($"");
    }
}