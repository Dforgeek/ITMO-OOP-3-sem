using Backups.Models;

namespace Backups.Interfaces;

public interface IRepository
{
    string PathToRepository { get; }
    bool ValidatePathInsideRepository(string pathToObjectFromRepository);
    IRepositoryObject GetRepositoryObject(BackupObject backupObject);

    Stream OpenWrite(string path);
    Stream OpenRead(string path);
}