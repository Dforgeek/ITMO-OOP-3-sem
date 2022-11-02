using Backups.Models;

namespace Backups.Interfaces;

public interface IRepository
{
    void Save(string dirPath, List<Storage> storages);
}