using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class ZipArchiver : IArchiver
{
    private List<BackupObject> _backupObjects;

    public ZipArchiver()
    {
        _backupObjects = new List<BackupObject>();
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects.AsReadOnly();

    public void AddBackupObject(BackupObject backupObject)
    {
        _backupObjects.Add(backupObject);
    }

    public void Archive()
    {
        throw new NotImplementedException();
    }
}