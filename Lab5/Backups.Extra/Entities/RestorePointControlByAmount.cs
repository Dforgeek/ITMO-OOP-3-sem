using Backups.Entities;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities;

public class RestorePointControlByAmount : IRestorePointControl
{
    public RestorePointControlByAmount(int amount)
    {
        if (amount < 0)
            throw new Exception();
        Amount = amount;
    }

    public int Amount { get; }

    public List<RestorePoint> UpdateRestorePoints(IReadOnlyCollection<RestorePoint> restorePoints)
    {
        var newRestorePoints = new List<RestorePoint>(restorePoints);
        newRestorePoints.RemoveRange(0, restorePoints.Count - Amount);
        return newRestorePoints;
    }
}