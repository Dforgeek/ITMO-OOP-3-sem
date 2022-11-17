using Backups.Interfaces;

namespace Backups.Entities;

public class Configuration
{
    public Configuration(IRepository repository, IStorageAlgorithm storageAlgorithm)
    {
        Repository = repository;
        StorageAlgorithm = storageAlgorithm;
    }

    public IRepository Repository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
}