using Backups.Models;

namespace Backups.Interfaces;

public interface IRepository
{
    bool IsDirectory(string path);
    void CreateDirectory(string path);

    Stream GetStream(string path);
}