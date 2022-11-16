using Backups.Entities;
using Backups.Extra.Interfaces;
using Newtonsoft.Json;

namespace Backups.Extra.Entities;

public class JsonSerializer : ISerializer
{
    public JsonSerializer()
    {
    }

    public void Serialize(BackupTask backupTask, string path)
    {
        throw new NotImplementedException();
    }

    public BackupTask Deserialize(string path)
    {
        throw new NotImplementedException();
    }
}