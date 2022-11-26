using Backups.Entities;
using Backups.Exceptions;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class BackupProxyWithLogger : IBackup
{
    private IBackup _backup;

    public BackupProxyWithLogger(IBackup backup, ILogger logger)
    {
        _backup = backup;
        Logger = logger;
    }

    public ILogger Logger { get; }

    public IReadOnlyCollection<RestorePoint> RestorePoints => _backup.RestorePoints;
    public void AddRestorePoint(RestorePoint restorePoint)
    {
        _backup.AddRestorePoint(restorePoint);
        Logger.Log($"Added RestorePoint: {restorePoint.Id}");
    }

    public RestorePoint GetRestorePoint(Guid id)
    {
        RestorePoint result = _backup.GetRestorePoint(id);
        Logger.Log($"Found RestorePoint: {id}");
        return result;
    }

    public void DeleteRestorePoint(Guid id)
    {
        _backup.DeleteRestorePoint(id);
        Logger.Log($"Deleted RestorePoint {id}");
    }
}