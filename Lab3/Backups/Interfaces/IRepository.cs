using Backups.Models;

namespace Backups.Interfaces;

public interface IRepository
{
    void Write(string dirPath, List<Storage> storages);
    
}