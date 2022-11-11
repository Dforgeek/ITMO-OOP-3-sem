using Backups.Interfaces;

namespace Backups.Entities;

public class Folder : IFolder
{
    private Func<IReadOnlyCollection<IRepositoryObject>> _functor;
    public Folder(IRepository repository, string path, Func<IReadOnlyCollection<IRepositoryObject>> functor)
    {
        Repository = repository;
        RelativePath = path;
        _functor = functor;
    }

    public string RelativePath { get; }
    public IRepository Repository { get; }

    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects
    {
        
    }

    public void Accept(IRepositoryObjectVisitor repositoryObjectVisitor)
    {
        throw new NotImplementedException();
    }
}