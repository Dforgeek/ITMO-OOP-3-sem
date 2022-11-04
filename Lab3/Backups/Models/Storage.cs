using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Models;

public class Storage
{
    public Storage(BackupObject backupObject, IRepository repository)
    {
        BackupObject = backupObject;
        Repository = repository;
    }

    public BackupObject BackupObject { get; }
    public IRepository Repository { get; }
}