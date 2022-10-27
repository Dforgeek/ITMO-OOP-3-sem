namespace Backups.Entities;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;

    public RestorePoint(DateTime dateTime, Guid id)
    {
        DateTime = dateTime;
        Id = id;
        _backupObjects = new List<BackupObject>();
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects.AsReadOnly();
    public DateTime DateTime { get; }

    public Guid Id { get; }
}