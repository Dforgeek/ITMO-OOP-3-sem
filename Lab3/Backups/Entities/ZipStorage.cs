using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipStorage : IStorage
{
    public ZipStorage(IRepository repository, string pathToStorage, IReadOnlyCollection<IZipObject> zipObjects)
    {
        Repository = repository;
        PathToStorage = pathToStorage;
        ZipObjects = zipObjects;
    }

    public IRepository Repository { get; }
    public IReadOnlyCollection<IZipObject> ZipObjects { get; }

    public string PathToStorage { get; }

    public IWrapper GetWrapper()
    {
        Stream archiveStream = ((IFile)Repository.GetRepositoryObject(PathToStorage)).GetStream();
        var zipArchive = new ZipArchive(archiveStream, ZipArchiveMode.Read);

        return new Wrapper(zipArchive, ZipObjects);
    }
}