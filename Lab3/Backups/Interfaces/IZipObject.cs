using System.IO.Compression;

namespace Backups.Interfaces;

public interface IZipObject
{
    string Name { get; }
    IRepositoryObject GetIRepositoryObject();
}