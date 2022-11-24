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

    public IStorage Store(IReadOnlyCollection<BackupObject> backupObjects, IRepository repository, string path, DateTime dateTime)
    {
        var repositoryObjects = backupObjects
            .Select(backupObject => backupObject.GetRepositoryObject()).ToList();

        var storages = repositoryObjects
            .Select(repositoryObject => AddStorage(repositoryObject, repository, path, dateTime)).ToList();

        return new SplitStorage(repository, path, storages);
    }

    private IStorage AddStorage(IRepositoryObject repositoryObject, IRepository repository, string path, DateTime dateTime)
    {
        string concreteZipFilePath = Path
            .Combine(path, string.Concat(dateTime.ToString("dd-MM-yyyy.hh-mm"), $".{repositoryObject.RepObjPath}.zip"));

        var temp = new List<IRepositoryObject> { repositoryObject };
        return _archiver
            .Encode(new List<IRepositoryObject>(temp), repository, concreteZipFilePath);
    }
}