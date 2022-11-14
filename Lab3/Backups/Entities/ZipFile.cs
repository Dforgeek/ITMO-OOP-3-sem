using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipFile : IZipObject
{
    private ZipArchiveEntry _zipArchiveEntry;
    public ZipFile(string name, ZipArchiveEntry entry)
    {
        _zipArchiveEntry = entry;
        Name = name;
    }

    public string Name { get; }

    public IRepositoryObject GetIRepositoryObject()
    {
        return new File(Name, _zipArchiveEntry.Open);
    }
}