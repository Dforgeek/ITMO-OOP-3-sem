namespace Backups.Interfaces;

public interface IRepositoryObject
{
    string Name { get; }
    void Accept(IRepositoryObjectVisitor repositoryObjectVisitor);
}