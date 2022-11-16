using System.Globalization;
using System.Net.Http.Headers;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<BackupObject> _currentBackupObjects;
    private readonly List<RestorePoint> _restorePoints;

    public BackupTask(IRepository repository, IStorageAlgorithm storageAlgorithm, Guid id)
    {
        BackupTaskPath = repository.PathToRepository;
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
        DateTime dateTime = DateTime.Now;
        IStorage storage = StorageAlgorithm.Store(_currentBackupObjects, Repository, BackupTaskPath, dateTime);

        RestorePoint.RestorePointBuilder restorePointBuilder = RestorePoint.Builder(storage, dateTime);
        foreach (BackupObject currentBackupObject in _currentBackupObjects)
        {
            restorePointBuilder.AddBackupObject(currentBackupObject);
        }

        RestorePoint restorePoint = restorePointBuilder.Build();
        _restorePoints.Add(restorePoint);
        return restorePoint;
    }

    public BackupObject? FindBackupObject(string backupObjectPath)
    {
        return _currentBackupObjects.FirstOrDefault(backupObject => backupObjectPath == backupObject.Path);
    }

    public BackupObject GetBackupObject(string backupObjectPath)
    {
        return FindBackupObject(backupObjectPath) ?? throw new Exception();
    }

    public BackupObject AddBackupObject(IRepository repository, string path)
    {
        _currentBackupObjects.Add(new BackupObject(repository, path));
        return _currentBackupObjects.Last();
    }

    public void DeleteBackupObject(string backupObjectPath)
    {
        _currentBackupObjects.Remove(GetBackupObject(backupObjectPath));
    }
}