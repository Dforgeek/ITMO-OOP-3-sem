using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class BackupTaskExtra
{
    private readonly BackupTask _backupTask;
    private IBackup _backup;

    public BackupTaskExtra(IBackup backup, IRepository repository, IStorageAlgorithm storageAlgorithm, ILogger logger, IRestorePointControl restorePointControl, Guid id)
    {
        _backup = backup;
        Logger = logger;
        RestorePointControl = restorePointControl;
        _backupTask = new BackupTask(_backup, repository, storageAlgorithm, id);
    }

    public Guid Id => _backupTask.Id;
    public IRepository Repository => _backupTask.Repository;
    public IStorageAlgorithm StorageAlgorithm => _backupTask.StorageAlgorithm;
    public IRestorePointControl RestorePointControl { get; }
    public string BackupTaskPath => _backupTask.BackupTaskPath;
    public ILogger Logger { get; }

    public BackupTaskExtraMemento GetMemento()
    {
        
    }
}
