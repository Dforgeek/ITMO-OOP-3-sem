using Backups.Entities;
using Backups.Models;

namespace Backups.Interfaces;

public interface IStorageAlgorithm
{
    void Store(string path, List<BackupObject> backupObjects, IRepository repository);
}