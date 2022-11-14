using System.Net.Http.Headers;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<BackupObject> _currentBackupObjects;
    private readonly List<RestorePoint> _restorePoints;

    public BackupTask(string backupTaskPath, IRepository repository, IStorageAlgorithm storageAlgorithm, Guid id)
    {
        BackupTaskPath = backupTaskPath;
        StorageAlgorithm = storageAlgorithm;
        Id = id;
        _restorePoints = new List<RestorePoint>();
        _currentBackupObjects = new List<BackupObject>();
        Repository = repository;
    }

    public Guid Id { get; }
    public IRepository Repository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public string BackupTaskPath { get; }
    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints.AsReadOnly();

    public RestorePoint AddRestorePoint()
    {
        string restorePointPath = Path.Combine(BackupTaskPath, DateTime.Now.ToString());
        List<IStorage> storages = StorageAlgorithm.Store(_currentBackupObjects, Repository);

        var restorePointBuilder = RestorePoint.Builder;
        foreach (BackupObject currentBackupObject in _currentBackupObjects)
        {
            restorePointBuilder.AddBackupObject(currentBackupObject);
        }

        RestorePoint restorePoint = restorePointBuilder.Build();
        _restorePoints.Add(restorePoint);
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