using Backups.Interfaces;

namespace Backups.Entities;

public class SplitStorage : IStorage
{
    private readonly List<ZipStorage> _zipStorages;

    public SplitStorage(List<ZipStorage> zipStorages)
    {
        _zipStorages = new List<ZipStorage>();
    }

    public IReadOnlyCollection<ZipStorage> ZipStorages => _zipStorages.AsReadOnly();

    public List<IRepositoryObject> GetRepositoryObjects()
    {
        var repositoryObjects = new List<IRepositoryObject>();
        foreach (ZipStorage zipStorage in _zipStorages)
        {
            repositoryObjects.AddRange(zipStorage.GetRepositoryObjects());
        }

        return repositoryObjects;
    }
}