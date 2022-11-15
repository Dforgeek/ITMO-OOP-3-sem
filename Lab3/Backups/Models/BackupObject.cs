using Backups.Interfaces;

namespace Backups.Models;

public class BackupObject
{
    public BackupObject(IRepository repository, string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new Exception();
        Path = path;
        Repository = repository;
    }

    public string Path { get; }
    public IRepository Repository { get; }

    public IRepositoryObject GetRepositoryObject()
    {
        return Repository.GetRepositoryObject(this);
    }
}