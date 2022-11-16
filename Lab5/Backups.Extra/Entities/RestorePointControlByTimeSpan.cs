using Backups.Entities;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities;

public class RestorePointControlByTimeSpan : IRestorePointControl
{
    public RestorePointControlByTimeSpan(TimeSpan interval)
    {
        Interval = interval;
    }

    public TimeSpan Interval { get; }

    public List<RestorePoint> UpdateRestorePoints(IReadOnlyCollection<RestorePoint> restorePoints)
    {
        DateTime maxDateTime = restorePoints.Max(rp => rp.DateTime);
        return restorePoints.Where(rp => maxDateTime - rp.DateTime < Interval).ToList();
    }
}