using Backups.Entities;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities;

public class RestorePointControlByAmount : IRestorePointControl
{
    public RestorePointControlByAmount(int maxAmount)
    {
        if (maxAmount < 0)
            throw new Exception();
        MaxAmount = maxAmount;
    }

    public int MaxAmount { get; }

    public List<RestorePoint> GetRestorePointsToExclude(IReadOnlyCollection<RestorePoint> restorePoints)
    {
        var restorePointsToExclude = new List<RestorePoint>(restorePoints);
        restorePointsToExclude.RemoveRange(restorePoints.Count - MaxAmount, restorePoints.Count - 1);
        if (restorePointsToExclude.Count == restorePoints.Count)
            throw new Exception();
        return restorePointsToExclude;
    }
}