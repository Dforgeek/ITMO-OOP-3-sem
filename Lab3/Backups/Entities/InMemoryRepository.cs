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

    public IRepositoryObject GetRepositoryObject(BackupObject backupObject)
    {
        if (_fileSystem.FileExists(backupObject.Path))
            return new File(Path.GetFileName(backupObject.Path), () => OpenRead(backupObject.Path));
        if (_fileSystem.DirectoryExists(backupObject.Path))
        {
            return new Folder(Path.GetFileName(backupObject.Path), () =>
            {
                return Directory
                    .EnumerateFileSystemEntries(backupObject.Path, searchPattern: "*", SearchOption.TopDirectoryOnly)
                    .Select(repoObj => GetRepositoryObject(new BackupObject(this, repoObj))).ToList();
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
}