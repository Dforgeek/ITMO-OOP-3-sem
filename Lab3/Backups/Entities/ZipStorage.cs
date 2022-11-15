using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipStorage : IStorage
{
    private readonly List<IZipObject> _zipObjects;
    public ZipStorage(IRepository repository, string pathToArchiveFromRepository, List<IZipObject> zipObjects)
    {
        Repository = repository;
        PathToArchiveFromRepository = pathToArchiveFromRepository;
        _zipObjects = zipObjects;
    }

    public IRepository Repository { get; }
    public string PathToArchiveFromRepository { get; }
    public IReadOnlyCollection<IZipObject> ZipObjects => _zipObjects.AsReadOnly();
    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        return _zipObjects.Select(zipObject => zipObject.GetIRepositoryObject()).ToList();
    }
}