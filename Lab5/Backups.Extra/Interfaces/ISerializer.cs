using Backups.Entities;

namespace Backups.Extra.Interfaces;

public interface ISerializer
{
    void Serialize(BackupTask backupTask, string path);

    BackupTask Deserialize(string path);
}