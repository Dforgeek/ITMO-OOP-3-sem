namespace Backups.Interfaces;

public interface IConfiguration
{
    public IRepository Repository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
}