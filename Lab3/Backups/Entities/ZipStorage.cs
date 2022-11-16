using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipStorage : IStorage
{
    private readonly List<IZipObject> _zipObjects;
    public ZipStorage(IRepository repository, string pathToStorage, List<IZipObject> zipObjects)
    {
        Repository = repository;
        PathToStorage = pathToStorage;
        _zipObjects = zipObjects;
    }

    public IRepository Repository { get; }
    public IReadOnlyCollection<IZipObject> ZipObjects => _zipObjects.AsReadOnly();
    public string PathToStorage { get; }

    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        Stream archiveStream = Repository.OpenRead(PathToStorage);
        var zipArchive = new ZipArchive(archiveStream, ZipArchiveMode.Read);
        return _zipObjects.Select(zipObject => zipObject.GetIRepositoryObject(zipArchive)).ToList();
    }
}