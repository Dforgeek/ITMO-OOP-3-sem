using System.IO.Compression;

namespace Backups.Interfaces;

public interface IWrapper : IDisposable
{
    public IReadOnlyCollection<IZipObject> ZipObjects { get; }
    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects();
}