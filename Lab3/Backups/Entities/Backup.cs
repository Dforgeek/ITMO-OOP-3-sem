using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

public class Backup : IBackup
{
    private readonly List<RestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new List<RestorePoint>();
    }

    public IReadOnlyCollection<RestorePoint> RestorePoints => new List<RestorePoint>().AsReadOnly();

    public RestorePoint GetRestorePoint(Guid id)
    {
        return _restorePoints.FirstOrDefault(rp => rp.Id == id) ?? throw BackupException.NoSuchRestorePoint();
    }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        if (_restorePoints.Contains(restorePoint))
            throw BackupException.RestorePointAlreadyInBackup();
        _restorePoints.Add(restorePoint);
    }

    public void DeleteRestorePint(Guid id)
    {
        _restorePoints.Remove(GetRestorePoint(id));
    }
}