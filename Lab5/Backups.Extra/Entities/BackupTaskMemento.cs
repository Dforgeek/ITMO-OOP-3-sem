using System.Data.Common;
using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class BackupTaskMemento
{
    private IBackup _backup;
    private IRepository _repository;
    private IStorageAlgorithm _storageAlgorithm;
    private Guid _id;
    private ILogger _logger;
    private IRestorePointControl _restorePointControl;

    public BackupTaskMemento(IBackup backup, IRepository repository, IStorageAlgorithm storageAlgorithm, ILogger logger, IRestorePointControl restorePointControl, Guid id)
    {
        _backup = backup;
        _id = id;
        _storageAlgorithm = storageAlgorithm;
        _repository = repository;
        _restorePointControl = restorePointControl;
        _logger = logger;
    }

    public BackupTaskDecorator DownloadBackupTask()
    {
        return new BackupTaskDecorator(_backup, _repository, _storageAlgorithm, _logger, _restorePointControl, _id);
    }
}