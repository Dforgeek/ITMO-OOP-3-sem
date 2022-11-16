using System.IO.Compression;
using System.Net.Http.Headers;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipFile : IZipObject
{
    public ZipFile(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public IRepositoryObject GetIRepositoryObject(ZipArchive zipArchive)
    {
        return new File(Name, () => zipArchive.GetEntry(Name) !.Open());
    }
}