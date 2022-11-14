using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipFolder : IZipObject
{
    private readonly List<IZipObject> _zipObjects;

    public ZipFolder(string name, List<IZipObject> zipObjects)
    {
        Name = name;
        _zipObjects = zipObjects;
    }

    public string Name { get; }
    public IReadOnlyCollection<IZipObject> ZipObjects => _zipObjects.AsReadOnly();

    public IRepositoryObject GetIRepositoryObject()
    {
        return new Folder(Name, () => _zipObjects.Select(x => x.GetIRepositoryObject()).ToList().AsReadOnly());
    }
}