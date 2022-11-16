namespace Backups.Interfaces;

public interface IStorage
{
    string PathToStorage { get; }
    IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects();
}