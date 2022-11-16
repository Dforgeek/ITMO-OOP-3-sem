using Backups.Entities;

namespace Backups.Interfaces;

public interface IBackup
{
    IReadOnlyCollection<RestorePoint> RestorePoints { get; }

    void AddRestorePoint(RestorePoint restorePoint);
    RestorePoint GetRestorePoint(Guid id);

    void DeleteRestorePint(Guid id);
}