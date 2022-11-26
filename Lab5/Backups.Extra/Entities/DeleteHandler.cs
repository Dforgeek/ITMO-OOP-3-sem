using System.Runtime.InteropServices;
using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class DeleteHandler : IRestorePointHandler
{
    public DeleteHandler(ILogger logger)
    {
        Logger = logger;
    }

    public ILogger Logger { get; }

    public void Handle(IBackup backup, IRepository repository, IRestorePointControl restorePointControl)
    {
        var pointsToExclude = restorePointControl.GetRestorePointsToExclude(backup.RestorePoints).ToList();
        foreach (RestorePoint restorePoint in pointsToExclude)
        {
            repository.Delete(restorePoint.Storage.PathToStorage);
            backup.DeleteRestorePoint(restorePoint.Id);
            Logger.Log(
                $"Restore point has been deleted. Id: {restorePoint.Id}, " +
                $"DateTime: {restorePoint.DateTime}, Path: {restorePoint.Storage.PathToStorage}");
        }
    }
}