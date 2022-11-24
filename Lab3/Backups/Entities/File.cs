using Backups.Interfaces;

namespace Backups.Entities;

public class File : IFile
{
    private readonly Func<Stream> _streamFunctor;

    public File(string name, Func<Stream> streamFunctor)
    {
        _streamFunctor = streamFunctor;
        RepObjPath = name;
    }

    public string RepObjPath { get; }

    public void Accept(IRepositoryObjectVisitor repositoryObjectVisitor)
    {
        repositoryObjectVisitor.Visit(this);
    }

    public Stream GetStream()
    {
        return _streamFunctor();
    }
}