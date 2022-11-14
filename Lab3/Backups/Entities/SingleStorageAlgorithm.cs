using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    private IArchiver _archiver;

    public SingleStorageAlgorithm(IArchiver archiver)
    {
        _archiver = archiver;
    }

    public IStorage Store(List<BackupObject> backupObjects, IRepository repository, string path)
    {
        var repositoryObjects = backupObjects
            .Select(backupObject => backupObject.GetRepositoryObject()).ToList();
        return _archiver.Encode(repositoryObjects, repository, path);
    }
}