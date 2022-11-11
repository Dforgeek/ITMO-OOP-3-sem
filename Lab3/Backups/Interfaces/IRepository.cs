using Backups.Models;

namespace Backups.Interfaces;

public interface IRepository
{
    string PathToRepository { get; }
    bool ValidatePathInsideRepository(string pathToObjectFromRepository);
    IRepositoryObject GetRepositoryObject(string path);

    Stream GetStream(string path);
}