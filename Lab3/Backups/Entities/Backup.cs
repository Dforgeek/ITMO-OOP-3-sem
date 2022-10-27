namespace Backups.Entities;

public class Backup
{
    private readonly List<RestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new List<RestorePoint>();
    }

    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints.AsReadOnly();
}