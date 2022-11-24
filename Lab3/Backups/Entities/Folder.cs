using Backups.Interfaces;

namespace Backups.Entities;

public class Folder : IFolder
{
    private readonly Func<IReadOnlyCollection<IRepositoryObject>> _functor;
    public Folder(string name, Func<IReadOnlyCollection<IRepositoryObject>> functor)
    {
        RepObjPath = name;
        _functor = functor;
    }

    public string RepObjPath { get; }

    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        return _functor();
    }

    public void Accept(IRepositoryObjectVisitor repositoryObjectVisitor)
    {
        repositoryObjectVisitor.Visit(this);
    }
}