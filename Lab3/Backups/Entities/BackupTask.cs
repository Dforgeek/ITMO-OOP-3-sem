using System.Net.Http.Headers;
using Backups.Interfaces;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<BackupObject> _currentBackupObjects;
    private Backup _backup;

    public BackupTask(string name, string backupTaskPath, IRepository repository, IStorageAlgorithm storageAlgorithm, Guid id)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();

        Name = name;
        BackupTaskPath = Path.Combine(backupTaskPath, name);
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
    public string BackupTaskPath { get; }

    public RestorePoint AddRestorePoint()
    {
        if (!Directory.Exists(BackupTaskPath))
            Repository.CreateDirectory(BackupTaskPath);

        string restorePointPath = Path.Combine(BackupTaskPath, DateTime.Now.ToString());
        Repository.CreateDirectory(restorePointPath);

        StorageAlgorithm.Store(restorePointPath, _currentBackupObjects, Repository);
        var restorePoint = new RestorePoint(DateTime.Now, Guid.NewGuid());

        foreach (BackupObject currentBackupObject in _currentBackupObjects)
        {
            restorePoint.AddBackupObject(currentBackupObject);
        }

        _backup.AddRestorePoint(restorePoint);
        return restorePoint;
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
        _currentBackupObjects.Add(new BackupObject(Repository, path, Id));
        return _currentBackupObjects.Last();
    }

    public void DeleteBackupObject(Guid backupObjectId)
    {
        _currentBackupObjects.Remove(GetBackupObject(backupObjectId));
    }
}