using Backups.Entities;
using Backups.Models;

namespace Backups.Interfaces;

public interface IStorageAlgorithm
{
    IStorage Store(IReadOnlyCollection<IRepositoryObject> repositoryObjects, IRepository repository, string path, DateTime dateTime);
}