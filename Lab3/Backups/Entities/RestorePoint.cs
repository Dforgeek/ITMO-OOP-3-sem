using System.Data;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;

    private RestorePoint(DateTime dateTime, Guid id, List<BackupObject> backupObjects, IStorage storage)
    {
        DateTime = dateTime;
        Id = id;
        _backupObjects = backupObjects;
        Storage = storage;
    }

    public IStorage Storage { get; }
    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects.AsReadOnly();
    public DateTime DateTime { get; }

    public Guid Id { get; }
    public static RestorePointBuilder Builder() => new RestorePointBuilder();

    public class RestorePointBuilder
    {
        private List<BackupObject> _backupObjects;
        private IStorage? _storage;
        private DateTime _dateTime;

        public RestorePointBuilder()
        {
            _backupObjects = new List<BackupObject>();
            _storage = null;
        }

        public void SetDateTime(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public void SetStorage(IStorage storage)
        {
            _storage = storage;
        }

        public void AddBackupObject(BackupObject backupObject)
        {
            if (_backupObjects.Contains(backupObject))
                throw RestorePointException.BackupObjectAlreadyInRestorePoint();
            _backupObjects.Add(backupObject);
        }

        public void Reset()
        {
            _backupObjects = new List<BackupObject>();
        }

        public RestorePoint Build()
        {
            if (_storage == null)
                throw RestorePointBuilderException.StorageIsNull();
            var newRestorePoint = new RestorePoint(_dateTime, Guid.NewGuid(), _backupObjects, _storage);
            Reset();
            return newRestorePoint;
        }
    }
}