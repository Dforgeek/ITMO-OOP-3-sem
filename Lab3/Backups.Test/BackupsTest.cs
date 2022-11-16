using Backups.Entities;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupsTest
{
    [Fact]
    public void CreateFile_CreateTwoRepositories_MakeBackup_CheckThatBackupExists()
    {
        var fs = new MemoryFileSystem();
        fs.CreateDirectory(@"\Work");
        fs.CreateDirectory(@"\Archive");
        fs.WriteAllText(@"\Work\temp.txt", "This is a content");

        var archiver = new ZipArchiver();
        var algo = new SingleStorageAlgorithm(archiver);
        const string pathToWorkRep = @"\Work";
        var workRepository = new InMemoryRepository(pathToWorkRep, fs);
        const string pathToArchive = @"\Archive";
        var archiveRepository = new InMemoryRepository(pathToArchive, fs);
        var backupTask = new BackupTask(archiveRepository, algo, Guid.NewGuid());
        backupTask.AddBackupObject(workRepository, @"\Work\temp.txt");
        RestorePoint rp = backupTask.AddRestorePoint();
        Assert.True(fs.FileExists(@$"\Archive\{rp.DateTime.ToString("dd-MM-yyyy.hh-mm")}.zip"));
    }
}