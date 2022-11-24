using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class Wrapper : IWrapper
{
    public Wrapper(ZipArchive zipArchive, IReadOnlyCollection<IZipObject> zipObjects)
    {
        ZipArchive = zipArchive;
        ZipObjects = zipObjects;
    }

    public ZipArchive ZipArchive { get; }
    public IReadOnlyCollection<IZipObject> ZipObjects { get; }

    public void Dispose()
    {
        ZipArchive.Dispose();
    }

    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        return ZipObjects.Select(zipObject => zipObject.GetIRepositoryObject(ZipArchive)).ToList();
    }
}