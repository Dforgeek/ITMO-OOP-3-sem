using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    private IArchiver _archiver;

    public SplitStorageAlgorithm(IArchiver archiver)
    {
        _archiver = archiver;
    }

    public IStorage Store(List<BackupObject> backupObjects, IRepository repository, string path)
    {
        var repositoryObjects = backupObjects
            .Select(backupObject => backupObject.GetRepositoryObject()).ToList();

        var storages = repositoryObjects
            .Select(repositoryObject => _archiver.Encode(repositoryObjects, repository, path)).ToList();

        return new SplitStorage(storages);
    }
}