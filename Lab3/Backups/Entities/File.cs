using Backups.Interfaces;

namespace Backups.Entities;

public class File : IFile
{
    private Func<Stream> _streamFunctor;

    public File(IRepository repository, string pathFromRepToObject, Func<Stream> streamFunctor)
    {
        _streamFunctor = streamFunctor;
        PathFromRepToObject = pathFromRepToObject;
        Repository = repository;
    }

    public IRepository Repository { get; }
    public string PathFromRepToObject { get; }

    public void Accept(IRepositoryObjectVisitor repositoryObjectVisitor)
    {
        throw new NotImplementedException();
    }

    public Stream GetStream()
    {
        return Repository.GetStream(PathFromRepToObject);
    }
}