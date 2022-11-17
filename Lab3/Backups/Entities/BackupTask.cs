using System.Globalization;
using System.Net.Http.Headers;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<BackupObject> _currentBackupObjects;
    private readonly IBackup _backup;
    public BackupTask(IBackup backup, Configuration configuration, Guid id)
    {
        BackupTaskPath = configuration.Repository.PathToRepository;
        StorageAlgorithm = configuration.StorageAlgorithm;
        Id = id;
        _backup = backup;
        _currentBackupObjects = new List<BackupObject>();
        Repository = configuration.Repository;
    }

    public Guid Id { get; }
    public IRepository Repository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public string BackupTaskPath { get; }
    public IReadOnlyCollection<RestorePoint> RestorePoints => _backup.RestorePoints;

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
        _backup.AddRestorePoint(restorePoint);
        return restorePoint;
    }

    public RestorePoint GetRestorePoint(Guid id)
    {
        return _backup.GetRestorePoint(id);
    }

    public void DeleteRestorePoint(Guid id)
    {
        _backup.DeleteRestorePint(id);
    }

    public BackupObject? FindBackupObject(string backupObjectPath)
    {
        return _currentBackupObjects.FirstOrDefault(backupObject => backupObjectPath == backupObject.Path);
    }

    public BackupObject GetBackupObject(string backupObjectPath)
    {
        return FindBackupObject(backupObjectPath) ?? throw BackupTaskException.NoSuchBackupObject();
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