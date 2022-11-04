using Backups.Interfaces;

namespace Backups.Entities;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    private IArchiver _archiver;

    public SplitStorageAlgorithm(IArchiver archiver)
    {
        _archiver = archiver;
    }

    public void Store(string path, List<BackupObject> backupObjects, IRepository repository)
    {
        foreach (BackupObject backupObject in backupObjects)
        {
            Stream sourceOut = repository.GetStream(Path.Combine(path, Path.GetFileName(backupObject.Path)));
            Stream sourceIn = repository.GetStream(backupObject.Path);
            _archiver.Encode(sourceIn, sourceOut, "storage.zip");
            sourceIn.Close();
            sourceOut.Close();
        }
    }
}