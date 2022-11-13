using Backups.Interfaces;

namespace Backups.Entities;

public class Folder : IFolder
{
    private Func<IReadOnlyCollection<IRepositoryObject>> _functor;
    public Folder(IRepository repository, string path, Func<IReadOnlyCollection<IRepositoryObject>> functor)
    {
        Repository = repository;
        Name = path;
        _functor = functor;
    }

    public string Name { get; }
    public IRepository Repository { get; }

    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        return _functor();
    }

    public void Accept(IRepositoryObjectVisitor repositoryObjectVisitor)
    {
        throw new NotImplementedException();
    }
}