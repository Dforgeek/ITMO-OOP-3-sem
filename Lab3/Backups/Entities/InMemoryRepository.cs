using Backups.Interfaces;
using Zio;
using Zio.FileSystems;
using static System.IO.Path;

namespace Backups.Entities;

public class InMemoryRepository : IRepository
{
    private IFileSystem _fileSystem;

    public InMemoryRepository(IFileSystem memoryFileSystem)
    {
        _fileSystem = memoryFileSystem;
    }

    public bool IsDirectory(string path)
    {
        return Directory.Exists(path);
    }

    public void CreateDirectory(string path)
    {
        _fileSystem.CreateDirectory(path);
    }

    public Stream GetStream(string path)
    {
        return _fileSystem.OpenFile(path, FileMode.Create, FileAccess.ReadWrite);
    }
}