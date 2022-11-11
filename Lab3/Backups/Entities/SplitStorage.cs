using Backups.Interfaces;

namespace Backups.Entities;

public class SplitStorage : IStorage
{
    private readonly List<ZipStorage> _zipStorages; // TODO: addZipStorage()
    private Func<IReadOnlyCollection<IRepositoryObject>> _func;

    public SplitStorage(Func<IReadOnlyCollection<IRepositoryObject>> func)
    {
        _func = func;
        _zipStorages = new List<ZipStorage>();
    }

    public IReadOnlyCollection<ZipStorage> ZipStorages => _zipStorages.AsReadOnly();

    // TODO: Метод, который возвращает список IRepoObj
}