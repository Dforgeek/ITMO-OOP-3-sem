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
    public bool ValidatePathInsideRepository(string pathToObjectFromRepository)
    {
        throw new NotImplementedException();
    }

    public IRepositoryObject GetRepositoryObject(BackupObject backupObject)
    {
        throw new NotImplementedException();
    }

    public Stream OpenWrite(string path)
    {
        throw new NotImplementedException();
    }

    public Stream OpenRead(string path)
    {
        throw new NotImplementedException();
    }
}