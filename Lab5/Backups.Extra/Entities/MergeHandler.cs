using System.Runtime.InteropServices;
using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class MergeHandler : IMergeHandler
{
    public MergeHandler(ILogger logger)
    {
        Logger = logger;
    }

    public ILogger Logger { get; }
    public void Merge(IBackup backup, IRepository repository, IRestorePointControl restorePointControl)
    {
        List<RestorePoint> restorePointsToExclude = restorePointControl.GetRestorePointsToExclude(backup.RestorePoints);
        var result = new List<RestorePoint>();
        
    }
}