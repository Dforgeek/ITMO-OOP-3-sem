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

    public IStorage Store(IReadOnlyCollection<IRepositoryObject> repositoryObjects, IRepository repository, string path, DateTime dateTime)
    {
        string restorePointPath = Path.Combine(path, string.Concat(dateTime.ToString("dd-MM-yyyy.hh-mm"), ".zip"));
        return _archiver.Encode(repositoryObjects, repository, restorePointPath);
    }
}