using Backups.Entities;
using Backups.Models;

namespace Backups.Interfaces;

public interface IBackupTask
{
    RestorePoint AddRestorePoint();

    RestorePoint GetRestorePoint(Guid id);

    BackupObject? FindBackupObject(string backupObjectPath);

    BackupObject GetBackupObject(string backupObjectPath);

    BackupObject AddBackupObject(IRepository repository, string path);

    void DeleteBackupObject(string backupObjectPath);
}