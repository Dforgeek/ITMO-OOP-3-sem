using Backups.Entities;
using Backups.Exceptions;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class BackupRestorePointControl : IBackup
{
    private readonly IRestorePointControl _restorePointControl;
    private List<RestorePoint> _restorePoints;

    public BackupRestorePointControl(IRestorePointControl restorePointControl)
    {
        _restorePointControl = restorePointControl;
        _restorePoints = new List<RestorePoint>();
    }

    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints.AsReadOnly();
    public void AddRestorePoint(RestorePoint restorePoint)
    {
        if (_restorePoints.Contains(restorePoint))
            throw BackupException.RestorePointAlreadyInBackup();
        _restorePoints.Add(restorePoint);
        _restorePoints = _restorePointControl.UpdateRestorePoints(_restorePoints);
    }

    public RestorePoint GetRestorePoint(Guid id)
    {
        return _restorePoints.FirstOrDefault(rp => rp.Id == id) ?? throw BackupException.NoSuchRestorePoint();
    }

    public void DeleteRestorePint(Guid id)
    {
        _restorePoints.Remove(GetRestorePoint(id));
    }
}