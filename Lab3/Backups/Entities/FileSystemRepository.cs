using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class FileSystemRepository : IRepository
{
    public FileSystemRepository(string pathToRepository)
    {
        PathToRepository = pathToRepository;
    }

    public string PathToRepository { get; }

    public IRepositoryObject GetRepositoryObject(BackupObject backupObject)
    {
        if (System.IO.File.Exists(backupObject.Path))
            return new File(Path.GetFileName(backupObject.Path), () => OpenRead(backupObject.Path));
        if (Directory.Exists(backupObject.Path))
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
        return System.IO.File.OpenWrite(path);
    }

    public Stream OpenRead(string path)
    {
        return System.IO.File.OpenRead(path);
    }
}