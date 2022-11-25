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

    public IEnumerable<RestorePoint> GetRestorePointsToExclude(IReadOnlyCollection<RestorePoint> restorePoints)
    {
        var restorePointsToExclude = new List<RestorePoint>(restorePoints);

        return restorePointsToExclude.OrderBy(restorePoint => restorePoint.DateTime).ToList().SkipLast(MaxAmount);
    }
}