using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class SplitStorage : IStorage
{
    private readonly List<IStorage> _storages;

    public SplitStorage(IRepository repository, string pathToStorage, List<IStorage> zipStorages)
    {
        _storages = zipStorages;
        PathToStorage = pathToStorage;
        Repository = repository;
    }

    public IReadOnlyCollection<IStorage> Storages => _storages.AsReadOnly();

    public string PathToStorage { get; }
    public IRepository Repository { get; }

    public IWrapper GetWrapper()
    {
        Stream archiveStream = ((IFile)Repository.GetRepositoryObject(PathToStorage)).GetStream();
        var zipArchive = new ZipArchive(archiveStream, ZipArchiveMode.Read);
        return new WrapperAdapter(_storages.Select(storage => storage.GetWrapper()).ToList());
    }
}