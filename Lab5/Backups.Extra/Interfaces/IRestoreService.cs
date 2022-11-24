using Backups.Entities;

namespace Backups.Extra.Interfaces;

public interface IRestoreService
{
    void Restore(RestorePoint restorePoint);
}