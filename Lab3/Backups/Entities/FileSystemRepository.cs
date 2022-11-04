using Backups.Interfaces;

namespace Backups.Entities;

public class FileSystemRepository : IRepository
{
    public FileSystemRepository(string pathToRepository)
    {
        PathToRepository = pathToRepository;
    }

    public string PathToRepository { get; }

    public bool IsDirectory(string path)
    {
        return Directory.Exists(path);
    }

    public void CreateDirectory(string path)
    {
        if (Directory.Exists(path))
            throw new Exception();
        Directory.CreateDirectory(path);
    }

    public Stream GetStream(string path)
    {
        return File.Open(Path.GetFileName(path), FileMode.Open);
    }
}