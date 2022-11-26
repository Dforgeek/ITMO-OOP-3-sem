using Backups.Interfaces;

namespace Backups.Extra.Interfaces;

public interface IRestorePointHandler
{
    void Handle(IBackup backup, IRepository repository, IRestorePointControl restorePointControl);
}