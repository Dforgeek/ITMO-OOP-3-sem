using System.Runtime.CompilerServices;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    private IArchiver _archiver;

    public SingleStorageAlgorithm(IArchiver archiver)
    {
        _archiver = archiver;
    }

    public void Store(string path, List<BackupObject> backupObjects, IRepository repository)
    {
        Stream sourceOut = repository.GetStream(path);
        foreach (BackupObject backupObject in backupObjects)
        {
            Stream sourceIn = backupObject.Repository.GetStream(backupObject.PathFromRepToObject);
            _archiver.Encode(sourceIn, sourceOut, "storage.zip");
            sourceIn.Close();
        }

        sourceOut.Close();
    }
}