using Backups.Entities;
using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities;

public class RestorePointControlHybrid : IRestorePointControl
{
    public List<RestorePoint> UpdateRestorePoints(IReadOnlyCollection<RestorePoint> restorePoints)
    {
        throw new NotImplementedException();
    }
}