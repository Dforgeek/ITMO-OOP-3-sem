using Backups.Interfaces;
using Backups.Models;
using Zio;
using Zio.FileSystems;
using static System.IO.Path;

namespace Backups.Entities;

public class InMemoryRepository : IRepository
{
    private IFileSystem _fileSystem;

    public InMemoryRepository(string pathToRepository, IFileSystem memoryFileSystem)
    {
        _fileSystem = memoryFileSystem;
        PathToRepository = pathToRepository;
    }

    public string PathToRepository { get; }

    public IRepositoryObject GetRepositoryObject(string path)
    {
        if (_fileSystem.FileExists(path))
            return new File(Path.GetFileName(path), () => OpenRead(path));
        if (_fileSystem.DirectoryExists(path))
        {
            return new Folder(GetFileName(path), () =>
            {
                return Directory
                    .EnumerateFileSystemEntries(path, searchPattern: "*", SearchOption.TopDirectoryOnly)
                    .Select(repoObj => GetRepositoryObject(path)).ToList();
            });
        }

        throw new Exception();
    }

    public Stream OpenWrite(string path)
    {
        return _fileSystem.OpenFile(path, FileMode.Create, FileAccess.Write, FileShare.Write);
    }

    public Stream OpenRead(string path)
    {
        return _fileSystem.OpenFile(path, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    public void Delete(string path)
    {
        _fileSystem.DeleteDirectory(path, true);
    }
}