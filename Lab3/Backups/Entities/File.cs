using Backups.Interfaces;

namespace Backups.Entities;

public class File : IFile
{
    private Func<Stream> _streamFunctor;

    public File(string name, Func<Stream> streamFunctor)
    {
        _streamFunctor = streamFunctor;
        Name = name;
    }

    public string Name { get; }

    public void Accept(IRepositoryObjectVisitor repositoryObjectVisitor)
    {
        repositoryObjectVisitor.Visit(this);
    }

    public Stream GetStream()
    {
        return _streamFunctor();
    }
}