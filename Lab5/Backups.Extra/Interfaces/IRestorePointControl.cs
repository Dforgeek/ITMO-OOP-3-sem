using Backups.Entities;

namespace Backups.Extra.Interfaces;

public interface IRestorePointControl
{
    List<RestorePoint> GetRestorePointsToExclude(IReadOnlyCollection<RestorePoint> restorePoints);
}