using System.IO.Compression;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipVisitor : IRepositoryObjectVisitor
{
    private Stack<ZipArchive> _zipArchives;
    private Stack<List<IZipObject>> _zipObjectLists;

    public ZipVisitor(ZipArchive zipArchive)
    {
        _zipArchives = new Stack<ZipArchive>();
        _zipArchives.Push(zipArchive);
        _zipObjectLists = new Stack<List<IZipObject>>();
        _zipObjectLists.Push(new List<IZipObject>());
    }

    public IReadOnlyCollection<IZipObject> ZipObjects()
    {
        if (_zipObjectLists.Count != 1)
            throw ZipVisitorException.MoreThanOneLayerOfZipObjectList_InvariantCorrupted();
        return _zipObjectLists.Peek();
    }

    public void Visit(IFile file)
    {
        ZipArchiveEntry entry = _zipArchives.Peek().CreateEntry(Path.GetFileName(file.RepObjPath));
        using (Stream fileStream = file.GetStream())
        using (Stream archiveStream = entry.Open())
            fileStream.CopyTo(archiveStream);

        var zipFile = new ZipFile(file.RepObjPath);
        _zipObjectLists.Peek().Add(zipFile);
    }

    public void Visit(IFolder folder)
    {
        ZipArchiveEntry entry = _zipArchives.Peek().CreateEntry(Path.GetFileName(folder.RepObjPath));
        using var zipArchive = new ZipArchive(entry.Open(), ZipArchiveMode.Create);
        _zipArchives.Push(zipArchive);
        _zipObjectLists.Push(new List<IZipObject>());

        IReadOnlyCollection<IRepositoryObject> repositoryObjects = folder.GetRepositoryObjects();
        if (repositoryObjects.Count == 0) return;
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            repositoryObject.Accept(this);
        }

        var zipFolder = new ZipFolder(folder.RepObjPath, _zipObjectLists.Pop());
        _zipObjectLists.Peek().Add(zipFolder);
        _zipArchives.Pop();
    }
}