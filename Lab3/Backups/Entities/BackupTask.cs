using System.Net.Http.Headers;
using Backups.Interfaces;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<BackupObject> _currentBackupObjects;
    private Backup _backup;

    public BackupTask(string name, IRepository repository, IStorageAlgorithm storageAlgorithm, Guid id)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();

        Name = name;
        StorageAlgorithm = storageAlgorithm;
        Id = id;
        _backup = new Backup();
        _currentBackupObjects = new List<BackupObject>();
        Repository = repository;
    }

    public Guid Id { get; }
    public IRepository Repository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public string Name { get; }

    public RestorePoint AddRestorePoint()
    {
        // TODO: storage?
        var restorePoint = new RestorePoint(DateTime.Now, Guid.NewGuid());
        _backup.AddRestorePoint(restorePoint);
        return restorePoint;
    }

    public void DeleteRestorePoint(Guid restorePointId)
    {
        // TODO: storage
        _backup.DeleteRestorePoint(restorePointId);
    }

    public BackupObject? FindBackupObject(Guid backupObjectId)
    {
        return _currentBackupObjects.FirstOrDefault(backupObject => backupObject.Id == backupObjectId);
    }

    public BackupObject GetBackupObject(Guid backupObjectId)
    {
        return FindBackupObject(backupObjectId) ?? throw new Exception();
    }

    public BackupObject AddBackupObject(string path)
    {
        // TODO: path validation
        _currentBackupObjects.Add(new BackupObject(Repository, path, Id));
        return _currentBackupObjects.Last();
    }

    public void DeleteBackupObject(Guid backupObjectId)
    {
        // TODO: path validation
        _currentBackupObjects.Remove(GetBackupObject(backupObjectId));
    }
}