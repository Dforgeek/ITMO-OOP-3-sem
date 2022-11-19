using Backups.Entities;
using Backups.Models;

namespace Backups.Interfaces;

public interface IArchiver
{
    IStorage Encode(IEnumerable<IRepositoryObject> repositoryObjects, IRepository repository, string path);
}