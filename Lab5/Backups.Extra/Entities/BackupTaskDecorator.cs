using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra.Entities;

public class BackupTaskDecorator : IBackupTask
{
    private readonly BackupTask _backupTask;
    private bool _mergeInsteadOfDelete;
    private IBackup _backup;

    public BackupTaskDecorator(IBackup backup, ConfigurationExtra configuration, Guid id)
    {
        Logger = configuration.Logger;
        RestorePointControl = configuration.RestorePointControl;
        _backupTask = new BackupTask(backup, new Configuration(configuration.Repository, configuration.StorageAlgorithm), id);
        _mergeInsteadOfDelete = false;
        _backup = backup;
    }

    public Guid Id => _backupTask.Id;
    public IRepository Repository => _backupTask.Repository;
    public IStorageAlgorithm StorageAlgorithm => _backupTask.StorageAlgorithm;
    public IRestorePointControl RestorePointControl { get; }
    public string BackupTaskPath => _backupTask.BackupTaskPath;
    public IMergeHandler MergeHandler { get; set; }
    public ILogger Logger { get; }

    public bool MergeInsteadOfDelete
    {
        get => _mergeInsteadOfDelete;
        set
        {
            if (!value)
            {
                _mergeInsteadOfDelete = value;
            }
            else if (_backupTask.StorageAlgorithm is SplitStorageAlgorithm)
            {
                _mergeInsteadOfDelete = value;
            }

            throw new Exception();
        }
    }

    public BackupTaskMemento GetMemento()
    {
        // return new BackupTaskMemento(_backup, Repository, StorageAlgorithm, Logger, RestorePointControl, Id);
    }

    public RestorePoint AddRestorePoint()
    {
        RestorePoint restorePoint = _backupTask.AddRestorePoint(); // TODO: merge instead of delete
        Logger.Log($"Added RestorePoint {restorePoint.Id}");
        if (MergeInsteadOfDelete)
        {
            MergeHandler.Merge(_backup, Repository, RestorePointControl);
            return restorePoint;
        }

        List<RestorePoint> restorePointsToExclude =
            RestorePointControl.GetRestorePointsToExclude(_backupTask.RestorePoints);
        foreach (RestorePoint point in restorePointsToExclude)
        {
            _backupTask.DeleteRestorePoint(point.Id);
            Logger.Log($"Deleted excluded RestorePoint {point.Id}");
        }

        return restorePoint;
    }

    public RestorePoint GetRestorePoint(Guid id)
    {
        return _backupTask.GetRestorePoint(id);
    }

    public void DeleteRestorePoint(Guid id)
    {
        _backupTask.DeleteRestorePoint(id);
        Logger.Log($"Deleted RestorePoint {id}");
    }

    public BackupObject? FindBackupObject(string backupObjectPath)
    {
        return _backupTask.FindBackupObject(backupObjectPath);
    }

    public BackupObject GetBackupObject(string backupObjectPath)
    {
        return _backupTask.GetBackupObject(backupObjectPath);
    }

    public BackupObject AddBackupObject(IRepository repository, string path)
    {
        BackupObject backupObject = _backupTask.AddBackupObject(repository, path);
        Logger.Log($"Added BackupObject to BackupTask {_backupTask.GetHashCode()}. Path: {path}");
        return backupObject;
    }

    public void DeleteBackupObject(string backupObjectPath)
    {
        _backupTask.DeleteBackupObject(backupObjectPath);
        Logger.Log($"Deleted BackupObject from BackupTask {_backupTask.GetHashCode()}. Path: {backupObjectPath}");
    }
}