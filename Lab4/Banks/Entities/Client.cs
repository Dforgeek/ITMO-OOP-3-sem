using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Models;

public class Client
{
    private readonly List<INotificationStrategy> _notificationStrategies;

    private Client(string name, string surname, string? address, string? passport)
    {
        Name = name;
        Surname = surname;
        Address = address;
        Passport = passport;
        _notificationStrategies = new List<INotificationStrategy>();
    }

    public bool Verified => Address != null && Passport != null;

    public string Name { get; }

    public string Surname { get; }

    public string? Address { get; }

    public string? Passport { get; }

    public static ClientBuilder Builder() => new ClientBuilder();

    public void Subscribe(INotificationStrategy newWayToNotify)
    {
        if (_notificationStrategies.Contains(newWayToNotify))
            throw new Exception();
        _notificationStrategies.Add(newWayToNotify);
    }

    public void GetNotification()
    {
        
    }

    public class ClientBuilder
    {
        private string? _name;
        private string? _surname;
        private string? _passport;
        private string? _address;

        public ClientBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _name = null;
            _surname = null;
            _passport = null;
            _address = null;
        }

        public Client Build()
        {
            if (_name == null || _surname == null)
                throw new Exception();
            return new Client(_name, _surname, _address, _passport);
        }

        public ClientBuilder SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception();
            _name = name;
            return this;
        }

        public ClientBuilder SetSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new Exception();
            _surname = surname;
            return this;
        }

        public ClientBuilder SetPassport(string passport)
        {
            if (string.IsNullOrWhiteSpace(_surname))
                throw new Exception();
            _passport = passport;
            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new Exception();
            _address = address;
            return this;
        }
    }
}