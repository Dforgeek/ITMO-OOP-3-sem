using Backups.Models;

namespace Backups.Entities;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;

    private RestorePoint(DateTime dateTime, Guid id, List<BackupObject> backupObjects)
    {
        DateTime = dateTime;
        Id = id;
        _backupObjects = backupObjects;
    }

    public static RestorePointBuilder Builder => new RestorePointBuilder();
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects.AsReadOnly();
    public DateTime DateTime { get; }

    public Guid Id { get; }

    public class RestorePointBuilder
    {
        private List<BackupObject> _backupObjects;

        public RestorePointBuilder()
        {
            _backupObjects = new List<BackupObject>();
        }

        public void AddBackupObject(BackupObject backupObject)
        {
            if (_backupObjects.Contains(backupObject))
                throw new Exception();
            _backupObjects.Add(backupObject);
        }

        public void Reset()
        {
            _backupObjects = new List<BackupObject>();
        }

        public RestorePoint Build()
        {
            var newRestorePoint = new RestorePoint(DateTime.Now, Guid.NewGuid(), _backupObjects);
            Reset();
            return newRestorePoint;
        }
    }
}