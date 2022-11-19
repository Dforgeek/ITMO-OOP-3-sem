using Backups.Interfaces;

namespace Backups.Extra.Interfaces;

public interface IMergeHandler
{
    void Merge(IBackup backup, IRepository repository, IRestorePointControl restorePointControl);
}