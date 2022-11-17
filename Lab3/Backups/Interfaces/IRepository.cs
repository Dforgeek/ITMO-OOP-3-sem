using Backups.Models;

namespace Backups.Interfaces;

public interface IRepository
{
    string PathToRepository { get; }
    IRepositoryObject GetRepositoryObject(string path);

    Stream OpenWrite(string path);
    Stream OpenRead(string path);
}