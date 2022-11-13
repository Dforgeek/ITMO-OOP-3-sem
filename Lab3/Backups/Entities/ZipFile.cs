using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipFile : IZipObject
{
    public ZipFile(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IRepositoryObject GetIRepositoryObject(ZipArchiveEntry zipArchiveEntry)
    {
        if (zipArchiveEntry.Name != Name)
            throw new Exception();
        return new File(Name, zipArchiveEntry.Open);
    }
}