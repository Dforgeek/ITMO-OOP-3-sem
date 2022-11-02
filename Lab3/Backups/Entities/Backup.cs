using Backups.Interfaces;
using Microsoft.VisualBasic.FileIO;

namespace Backups.Entities;

public class Backup : IBackup
{
    private readonly List<RestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new List<RestorePoint>();
    }

    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints.AsReadOnly();

    public RestorePoint AddRestorePoint(RestorePoint restorePoint)
    {
        if (_restorePoints.Contains(restorePoint))
            throw new Exception();
        _restorePoints.Add(restorePoint);
        return restorePoint;
    }

    public void DeleteRestorePoint(Guid restorePointId)
    {
        _restorePoints.Remove(GetRestorePoint(restorePointId));
    }

    public RestorePoint? FindRestorePoint(Guid restorePointId)
    {
        return _restorePoints.FirstOrDefault(restorePoint => restorePoint.Id == restorePointId);
    }

    public RestorePoint GetRestorePoint(Guid restorePointId)
    {
        return FindRestorePoint(restorePointId) ?? throw new Exception();
    }
}