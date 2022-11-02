using Backups.Entities;

namespace Backups.Interfaces;

public interface IBackup
{
    RestorePoint AddRestorePoint(RestorePoint restorePoint);

    void DeleteRestorePoint(Guid restorePointId);

    RestorePoint? FindRestorePoint(Guid restorePointId);

    RestorePoint GetRestorePoint(Guid restorePointId);
}