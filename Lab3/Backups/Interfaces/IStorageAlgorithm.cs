using Backups.Entities;
using Backups.Models;

namespace Backups.Interfaces;

public interface IStorageAlgorithm
{
    IStorage Store(List<BackupObject> backupObjects, IRepository repository, string path, DateTime dateTime);
}