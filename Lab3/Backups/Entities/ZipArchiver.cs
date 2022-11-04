using System.IO.Compression;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class ZipArchiver : IArchiver
{
    private List<BackupObject> _backupObjects;
    private IRepository _repository;
    private string _path;

    public ZipArchiver(IRepository repository, string path)
    {
        _backupObjects = new List<BackupObject>();
        _repository = repository;
        _path = path;
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects.AsReadOnly();

    public void AddBackupObject(BackupObject backupObject)
    {
        _backupObjects.Add(backupObject);
    }

    public void Encode(Stream streamIn, Stream streamOut, string fileName)
    {
        using (var archive = new ZipArchive(streamOut))
        {
            ZipArchiveEntry file = archive.CreateEntry(fileName);
            using (Stream stream = file.Open())
                streamIn.CopyTo(stream);
        }
    }
}