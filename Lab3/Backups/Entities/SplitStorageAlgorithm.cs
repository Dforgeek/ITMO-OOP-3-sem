using System.IO.Enumeration;
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

    public IStorage Store(List<BackupObject> backupObjects, IRepository repository, string path, DateTime dateTime)
    {
        var repositoryObjects = backupObjects
            .Select(backupObject => backupObject.GetRepositoryObject()).ToList();
        var storages = new List<IStorage>();
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            string concreteZipFilePath = Path
                .Combine(path, string.Concat(dateTime.ToString("dd-MM-yyyy.hh-mm"), $".{repositoryObject.Name}.zip"));

            var temp = new List<IRepositoryObject> { repositoryObject };
            storages.Add(_archiver
                .Encode(new List<IRepositoryObject>(temp), repository, concreteZipFilePath));
        }

        return new SplitStorage(repository, path, storages);
    }
}