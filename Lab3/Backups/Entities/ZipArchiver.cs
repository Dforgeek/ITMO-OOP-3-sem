using System.IO.Compression;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class ZipArchiver : IArchiver
{
    public ZipArchiver() { }

    public IStorage Encode(List<IRepositoryObject> repositoryObjects, IRepository repository, string path)
    {
        Stream writeStream = repository.OpenWrite(path);
        var zipArchive = new ZipArchive(writeStream, ZipArchiveMode.Create);
        var zipVisitor = new ZipVisitor(zipArchive);
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            repositoryObject.Accept(zipVisitor);
        }

        return new ZipStorage(repository, path, zipVisitor.ZipObjects());
    }
}