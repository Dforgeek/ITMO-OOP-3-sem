using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipFolder : IZipObject
{
    private readonly List<IZipObject> _zipObjects;

    public ZipFolder(string name, List<IZipObject> zipObjects)
    {
        ZipObjPath = name;
        _zipObjects = zipObjects;
    }

    public string ZipObjPath { get; }
    public IReadOnlyCollection<IZipObject> ZipObjects => _zipObjects.AsReadOnly();

    public IRepositoryObject GetIRepositoryObject(ZipArchive zipArchive)
    {
        var zipArchiveTemp = new ZipArchive(zipArchive.GetEntry(Path.GetFileName(ZipObjPath)) !.Open(), ZipArchiveMode.Read);
        return new Folder(ZipObjPath, () => _zipObjects
            .Select(x => x.GetIRepositoryObject(zipArchiveTemp)).ToList().AsReadOnly());
    }
}