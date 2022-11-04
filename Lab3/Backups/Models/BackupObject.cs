using Backups.Interfaces;

namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(IRepository repository, string path, Guid id)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new Exception();
        Path = path;
        Id = id;
        Repository = repository;
    }

    public Guid Id { get; }
    public string Path { get; }
    public IRepository Repository { get; }
}