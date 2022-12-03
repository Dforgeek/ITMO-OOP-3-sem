using System.Data.Common;
using Banks.Entities;
using Banks.Notifications;
using Banks.ValueObjects;

namespace Banks.Entities;

public class Client : IEquatable<Client>
{
    private Client(Guid id, string name, string surname, string? address = null, Passport? passport = null)
    {
        Name = name;
        Surname = surname;
        Address = address;
        Passport = passport;
        Id = id;
    }

    public bool Verified => Address != null && Passport != null;

    public Guid Id { get; }

    public string Name { get; }

    public string Surname { get; }

    public string? Address { get; set; }

    public Passport? Passport { get; set; }

    public static ClientBuilder Builder() => new ClientBuilder();

    public bool Equals(Client? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Surname == other.Surname && Address == other.Address &&
               Equals(Passport, other.Passport);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Client)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Surname, Address, Passport);
    }

    public class ClientBuilder
    {
        private string? _name;
        private string? _surname;
        private Passport? _passport;
        private string? _address;
        private Guid _id;

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
            _id = Guid.Empty;
        }

        public Client Build()
        {
            if (_name == null || _surname == null || _id == Guid.Empty)
                throw new Exception();

            return new Client(_id, _name, _surname, _address, _passport);
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

        public ClientBuilder SetPassport(Passport passport)
        {
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

        public ClientBuilder SetId(Guid id)
        {
            _id = id;
            return this;
        }
    }
}