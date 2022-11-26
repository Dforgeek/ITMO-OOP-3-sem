using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra.Entities;

public class MergeHandler : IRestorePointHandler
{
    public MergeHandler(ILogger logger)
    {
        Logger = logger;
    }

    public ILogger Logger { get; }

    public void Handle(IBackup backup, IRepository repository, IRestorePointControl restorePointControl)
    {
        var pointsToExclude = restorePointControl.GetRestorePointsToExclude(backup.RestorePoints).ToList();
        if (pointsToExclude.Count == 0)
        {
            Logger.Log("No points to merge. Returning...");
            return;
        }

        DateTime dateTime = DateTime.Now;
        pointsToExclude = pointsToExclude.OrderByDescending(rp => rp.DateTime).ToList();
        RestorePoint mergedRestorePoint = pointsToExclude.Aggregate(
            (restoreP1, restoreP2) =>
            {
                RestorePoint.RestorePointBuilder builder = RestorePoint.Builder();
                builder.SetDateTime(dateTime);
                builder
                    .SetStorage(new SplitStorage(restoreP1.Storage.Repository, restoreP1.Storage.PathToStorage, new List<IStorage> { restoreP1.Storage, restoreP2.Storage }));
                foreach (BackupObject backupObject in restoreP1.BackupObjects.Union(restoreP2.BackupObjects))
                {
                    builder.AddBackupObject(backupObject);
                }

                return builder.Build();
            });
        Logger.Log($"Got new merged restore point {mergedRestorePoint.Id.ToString()}");
        IReadOnlyCollection<IRepositoryObject> repositoryObjects = mergedRestorePoint.Storage
            .GetWrapper().GetRepositoryObjects();

        var mergedRepObjects = mergedRestorePoint.BackupObjects
            .Select(restoreP1 => repositoryObjects.First(restoreP2 =>
                Path.GetFileName(restoreP2.RepObjPath) == Path.GetFileName(restoreP1.Path))).ToList();
        var archiver = new ZipArchiver();
        var algo = new SplitStorageAlgorithm(archiver);
        algo.Store(mergedRepObjects, repository, repository.PathToRepository, dateTime);
        Logger.Log($"Archived data from new merged restore point {mergedRestorePoint.Id}");

        foreach (RestorePoint restorePoint in pointsToExclude)
        {
            repository.Delete(restorePoint.Storage.PathToStorage);
            backup.DeleteRestorePoint(restorePoint.Id);
            Logger.Log($@"RestorePoint deleted {restorePoint.Id}");
        }
    }
}