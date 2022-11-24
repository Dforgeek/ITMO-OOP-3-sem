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

    public IRepositoryObject GetRepositoryObject(string path)
    {
        if (System.IO.File.Exists(path))
            return new File(path, () => OpenRead(path));
        if (Directory.Exists(path))
        {
            return new Folder(path, () =>
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
        return System.IO.File.OpenWrite(path);
    }

    public Stream OpenRead(string path)
    {
        return System.IO.File.OpenRead(path);
    }

    public void Delete(string path)
    {
        Directory.Delete(path, true);
    }
}