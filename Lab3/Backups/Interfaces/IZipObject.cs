using System.IO.Compression;

namespace Backups.Interfaces;

public interface IZipObject
{
    string ZipObjPath { get; }
    IRepositoryObject GetIRepositoryObject(ZipArchive zipArchive);
}