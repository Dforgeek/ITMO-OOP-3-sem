using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class ConfigurationExtra : IConfiguration
{
    public ConfigurationExtra(IRepository repository, IStorageAlgorithm storageAlgorithm, ILogger logger, IRestorePointControl restorePointControl)
    {
        Repository = repository;
        StorageAlgorithm = storageAlgorithm;
        RestorePointControl = restorePointControl;
        Logger = logger;
    }

    public IRestorePointControl RestorePointControl { get; }
    public IRepository Repository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public ILogger Logger { get; }
}