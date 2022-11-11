using Backups.Interfaces;

namespace Backups.Entities;

public class ZipStorage : IStorage
{
    public ZipStorage(IRepository repository, string pathToArchiveFromRepository, ZipFolder zipFolder)
    {
        Repository = repository;
        PathToArchiveFromRepository = pathToArchiveFromRepository;
        ZipFolder = zipFolder;
    }

    public IRepository Repository { get; }
    public string PathToArchiveFromRepository { get; }
    public ZipFolder ZipFolder { get; }
}