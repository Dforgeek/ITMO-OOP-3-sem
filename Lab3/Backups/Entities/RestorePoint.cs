namespace Backups.Entities;

public class RestorePoint : IEquatable<RestorePoint>
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

    public bool Equals(RestorePoint? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((RestorePoint)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}