using Backups.Interfaces;

namespace Backups.Entities;

public class SplitStorage : IStorage
{
    private readonly List<IStorage> _storages;

    public SplitStorage(IRepository repository, string pathToStorage, List<IStorage> zipStorages)
    {
        _storages = new List<IStorage>();
        PathToStorage = pathToStorage;
        Repository = repository;
    }

    public IReadOnlyCollection<IStorage> Storages => _storages.AsReadOnly();

    public string PathToStorage { get; }
    public IRepository Repository { get; }

    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        var repositoryObjects = new List<IRepositoryObject>();
        foreach (IStorage storage in _storages)
        {
            repositoryObjects.AddRange(storage.GetRepositoryObjects());
        }

        return repositoryObjects.AsReadOnly();
    }
}