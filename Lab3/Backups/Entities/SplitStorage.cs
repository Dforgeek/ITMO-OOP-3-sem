using Backups.Interfaces;

namespace Backups.Entities;

public class SplitStorage : IStorage
{
    private readonly List<IStorage> _storages;

    public SplitStorage(List<IStorage> zipStorages)
    {
        _storages = new List<IStorage>();
    }

    public IReadOnlyCollection<IStorage> Storages => _storages.AsReadOnly();

    public List<IRepositoryObject> GetRepositoryObjects()
    {
        var repositoryObjects = new List<IRepositoryObject>();
        foreach (IStorage storage in _storages)
        {
            repositoryObjects.AddRange(storage.GetRepositoryObjects());
        }

        return repositoryObjects;
    }
}