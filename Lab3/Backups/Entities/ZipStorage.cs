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
    public List<IRepositoryObject> GetRepositoryObjects()
    {
        ZipArchive zipArchive = new ZipArchive(Repository.OpenRead(PathToArchiveFromRepository), ZipArchiveMode.Create);
        var repositoryObjects = new List<IRepositoryObject>();
        foreach (IZipObject zipObject in _zipObjects)
        {
            foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries)
            {
                if (zipArchiveEntry.Name == zipObject.Name)
                    repositoryObjects.Add(zipObject.GetIRepositoryObject());
            }
        }

        return repositoryObjects;
    }
}