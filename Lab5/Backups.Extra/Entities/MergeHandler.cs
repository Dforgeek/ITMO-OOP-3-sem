using Backups.Extra.Interfaces;
using Backups.Interfaces;

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
        
    }
}