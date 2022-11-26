using System.IO.Compression;
using System.Net.Http.Headers;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipFile : IZipObject
{
    public ZipFile(string path)
    {
        ZipObjPath = path;
    }

    public string ZipObjPath { get; }
    public IRepositoryObject GetIRepositoryObject(ZipArchive zipArchive)
    {
        return new File(ZipObjPath, () =>
        {
            ZipArchiveEntry? entry = zipArchive.GetEntry(Path.GetFileName(ZipObjPath));
            if (entry != null)
                return zipArchive.GetEntry(Path.GetFileName(ZipObjPath)) !.Open();
            throw new Exception();
        });
    }
}