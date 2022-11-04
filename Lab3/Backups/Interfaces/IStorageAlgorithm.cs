using Backups.Entities;

namespace Backups.Interfaces;

public interface IStorageAlgorithm
{
    void Store(string path, List<BackupObject> backupObjects, IRepository repository);
}