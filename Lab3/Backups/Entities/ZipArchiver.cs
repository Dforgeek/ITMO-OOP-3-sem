using System.IO.Compression;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class ZipArchiver : IArchiver
{
    public IStorage Encode(IEnumerable<IRepositoryObject> repositoryObjects, IRepository repository, string path)
    {
        Stream writeStream = repository.OpenWrite(string.Concat(path, ".zip"));
        var zipArchive = new ZipArchive(writeStream, ZipArchiveMode.Create);
        var zipVisitor = new ZipVisitor(zipArchive);
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            repositoryObject.Accept(zipVisitor);
        }

        var zipStorage = new ZipStorage(repository, path, zipVisitor.ZipObjects());
        zipArchive.Dispose();
        writeStream.Dispose();
        return zipStorage;
    }
}