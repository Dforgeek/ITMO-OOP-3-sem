using Backups.Entities;

namespace Backups.Extra.Interfaces;

public interface IRestorePointControl
{
    IEnumerable<RestorePoint> GetRestorePointsToExclude(IReadOnlyCollection<RestorePoint> restorePoints);
}