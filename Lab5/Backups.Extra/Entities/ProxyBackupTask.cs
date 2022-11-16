using Backups.Entities;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities;

public class ProxyBackupTask
{
    private readonly BackupTask _backupTask;
    private readonly ILogger _logger;

    public ProxyBackupTask(BackupTask backupTask, ILogger logger)
    {
        _backupTask = backupTask;
        _logger = logger;
    }
}