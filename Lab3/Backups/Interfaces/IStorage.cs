using Backups.Entities;

namespace Backups.Interfaces;

public interface IStorage
{
    string PathToStorage { get; }
    IRepository Repository { get; }
    IWrapper GetWrapper();
}