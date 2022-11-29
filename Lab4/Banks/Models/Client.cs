namespace Banks.Models;

public class Client // Do i need to store BankAccounts in client? Maybe client in BankAccount?
{
    private Client(string name, string surname, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();
        if (string.IsNullOrWhiteSpace(surname))
            throw new Exception();
        if (string.IsNullOrWhiteSpace(address))
            throw new Exception();
        Name = name;
        Surname = surname;
        Address = address;
    }

    public string Name { get; }
    public string Surname { get; }
    public string Address { get; }
}