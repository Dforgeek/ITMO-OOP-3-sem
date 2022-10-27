namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(string path, Guid id)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new Exception();
        Path = path;
        Id = id;
    }

    public Guid Id { get; }
    public string Path { get; }
}