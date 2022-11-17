using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra.Entities;

public class BackupTaskExtra : IBackupTask
{
    private readonly BackupTask _backupTask;
    private IBackup _backup;

    public BackupTaskExtra(IBackup backup, IRepository repository, IStorageAlgorithm storageAlgorithm, ILogger logger, IRestorePointControl restorePointControl, Guid id)
    {
        _backup = backup;
        Logger = logger;
        RestorePointControl = restorePointControl;
        _backupTask = new BackupTask(_backup, new Configuration(repository, storageAlgorithm), id);
    }

    public Guid Id => _backupTask.Id;
    public IRepository Repository => _backupTask.Repository;
    public IStorageAlgorithm StorageAlgorithm => _backupTask.StorageAlgorithm;
    public IRestorePointControl RestorePointControl { get; }
    public string BackupTaskPath => _backupTask.BackupTaskPath;
    public ILogger Logger { get; }

    public BackupTaskExtraMemento GetMemento()
    {
        return new BackupTaskExtraMemento(_backup, Repository, StorageAlgorithm, Logger, RestorePointControl, Id);
    }

    public RestorePoint AddRestorePoint()
    {
        _backup
    }

    public RestorePoint GetRestorePoint(Guid id)
    {
        throw new NotImplementedException();
    }

    public void DeleteRestorePoint(Guid id)
    {
        throw new NotImplementedException();
    }

    public BackupObject? FindBackupObject(string backupObjectPath)
    {
        throw new NotImplementedException();
    }

    public BackupObject GetBackupObject(string backupObjectPath)
    {
        throw new NotImplementedException();
    }

    public BackupObject AddBackupObject(IRepository repository, string path)
    {
        throw new NotImplementedException();
    }

    public void DeleteBackupObject(string backupObjectPath)
    {
        throw new NotImplementedException();
    }
}
