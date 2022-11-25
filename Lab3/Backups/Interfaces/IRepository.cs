using Backups.Models;

namespace Backups.Interfaces;

public interface IRepository
{
    string PathToRepository { get; }
    void CreateDirectory(string path);
    IRepositoryObject GetRepositoryObject(string path);
    void Delete(string path);

    Stream OpenWrite(string path);
}