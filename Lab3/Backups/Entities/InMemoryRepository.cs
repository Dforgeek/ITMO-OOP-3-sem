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

    public bool ValidatePathInsideRepository(string pathToObjectFromRepository)
    {
        throw new NotImplementedException();
    }

    public IRepositoryObject GetRepositoryObject(BackupObject backupObject)
    {
        throw new NotImplementedException();
    }

    public Stream OpenWrite()
    {
        throw new NotImplementedException();
    }
}