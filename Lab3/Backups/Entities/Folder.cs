using Backups.Interfaces;

namespace Backups.Entities;

public class Folder : IFolder
{
    private Func<IReadOnlyCollection<IRepositoryObject>> _functor;
    public Folder(string name, Func<IReadOnlyCollection<IRepositoryObject>> functor)
    {
        Name = name;
        _functor = functor;
    }

    public string Name { get; }

    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        return _functor();
    }

    public void Accept(IRepositoryObjectVisitor repositoryObjectVisitor)
    {
        repositoryObjectVisitor.Visit(this);
    }
}