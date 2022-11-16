using Backups.Entities;

namespace Backups.Extra.Interfaces;

public interface IRestorePointControl
{
    List<RestorePoint> UpdateRestorePoints(IReadOnlyCollection<RestorePoint> restorePoints);
}