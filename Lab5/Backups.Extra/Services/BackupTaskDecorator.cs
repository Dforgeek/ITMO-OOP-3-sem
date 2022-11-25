using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra.Entities;

public class BackupTaskDecorator : IBackupTask
{
    private readonly BackupTask _backupTask;
    private IBackup _backup;

    public BackupTaskDecorator(IBackup backup, ConfigurationExtra configuration, Guid id)
    {
        Logger = configuration.Logger;
        RestorePointControl = configuration.RestorePointControl;
        RestorePointHandler = configuration.RestorePointHandler;
        _backupTask = new BackupTask(backup, new Configuration(configuration.Repository, configuration.StorageAlgorithm), id);
        _backup = backup;
    }

    public Guid Id => _backupTask.Id;
    public IRepository Repository => _backupTask.Repository;
    public IStorageAlgorithm StorageAlgorithm => _backupTask.StorageAlgorithm;
    public IRestorePointControl RestorePointControl { get; }
    public string BackupTaskPath => _backupTask.BackupTaskPath;
    public IRestorePointHandler RestorePointHandler { get; set; }
    public ILogger Logger { get; }

    public RestorePoint AddRestorePoint()
    {
        RestorePoint restorePoint = _backupTask.AddRestorePoint(); // TODO: merge instead of delete
        Logger.Log($"Added RestorePoint {restorePoint.Id}");

        RestorePointHandler.Handle(_backup, Repository, RestorePointControl);
        return restorePoint;

        // var restorePointsToExclude =
        //     RestorePointControl.GetRestorePointsToExclude(_backupTask.RestorePoints).ToList();
        // foreach (RestorePoint point in restorePointsToExclude)
        // {
        //     DeleteRestorePoint(point.Id);
        // }
    }

    public void Restore(Guid id, IRestoreService restoreService)
    {
        restoreService.Restore(GetRestorePoint(id));
    }

    public RestorePoint GetRestorePoint(Guid id)
    {
        return _backupTask.GetRestorePoint(id);
    }

    public void DeleteRestorePoint(Guid id)
    {
        _backup.DeleteRestorePoint(id); // TODO: delete from fs also
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
        Logger.Log($"Added BackupObject to BackupTask {_backupTask.GetHashCode()}. RepObjPath: {path}");
        return backupObject;
    }

    public void DeleteBackupObject(string backupObjectPath)
    {
        _backupTask.DeleteBackupObject(backupObjectPath);
        Logger.Log($"Deleted BackupObject from BackupTask {_backupTask.GetHashCode()}. RepObjPath: {backupObjectPath}");
    }
}