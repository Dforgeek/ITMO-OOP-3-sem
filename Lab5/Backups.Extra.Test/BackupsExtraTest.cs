using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Extra.Logger;
using Backups.Extra.Services;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Extra.Test;

public class BackupsExtraTest
{
    [Fact]
    public void CreateRestorePoint_afterThatRestore()
    {
        IFileSystem fs = new MemoryFileSystem();
        const string pathToWorkRep = @"\Work";
        const string pathToArchive = @"\Archive";
        const string pathToRestore = @"\Restore";
        fs.CreateDirectory(@"\Work");
        fs.CreateDirectory(@"\Archive");
        fs.CreateDirectory(@"\Restore");
        fs.WriteAllText(@"\Work\temp.txt", "This is a content");
        var workRepository = new InMemoryRepository(pathToWorkRep, fs);
        var archiveRepository = new InMemoryRepository(pathToArchive, fs);
        var restoreRepository = new InMemoryRepository(pathToRestore, fs);

        var archiver = new ZipArchiver();
        var algo = new SingleStorageAlgorithm(archiver);
        var backup = new Backup();
        var logger = new ConsoleLogger(new DateTimeConfiguration());
        var configuration = new ConfigurationExtra(archiveRepository, algo, logger, new RestorePointControlByAmount(10), new DeleteHandler(logger));
        var backupTask = new BackupTaskDecorator(backup, configuration, Guid.NewGuid());
        backupTask.AddBackupObject(workRepository, @"\Work\temp.txt");
        RestorePoint rp = backupTask.AddRestorePoint();
        backupTask.Restore(rp.Id, new ToDifferLocationRestoreService(restoreRepository));
        Assert.True(fs.FileExists(@$"\Archive\{rp.DateTime.ToString("dd-MM-yyyy.hh-mm")}.zip"));
        Assert.True(fs.FileExists(@"\Restore\temp.txt"));
    }

    [Fact]
    public void CreateRestorePoint_Merge()
    {
        IFileSystem fs = new MemoryFileSystem();
        const string pathToWorkRep = @"\Work";
        const string pathToArchive = @"\Archive";
        fs.CreateDirectory(@"\Work");
        fs.CreateDirectory(@"\Archive");
        fs.CreateDirectory(@"\Restore");
        fs.WriteAllText(@"\Work\temp1.txt", "This is a content 1");
        fs.WriteAllText(@"\Work\temp2.txt", "This is a content 2");
        var workRepository = new InMemoryRepository(pathToWorkRep, fs);
        var archiveRepository = new InMemoryRepository(pathToArchive, fs);

        var archiver = new ZipArchiver();
        var algo = new SingleStorageAlgorithm(archiver);
        var logger = new ConsoleLogger(new DateTimeConfiguration());
        var backup = new Backup();
        var configuration = new ConfigurationExtra(archiveRepository, algo, logger, new RestorePointControlByAmount(1), new MergeHandler(logger));
        var backupTask = new BackupTaskDecorator(backup, configuration, Guid.NewGuid());
        backupTask.AddBackupObject(workRepository, @"\Work\temp1.txt");
        backupTask.AddBackupObject(workRepository, @"\Work\temp2.txt");

        backupTask.AddRestorePoint();

        backupTask.DeleteBackupObject(@"\Work\temp2.txt");
        Assert.Equal("1", backupTask.RestorePoints.Count.ToString());
    }
}