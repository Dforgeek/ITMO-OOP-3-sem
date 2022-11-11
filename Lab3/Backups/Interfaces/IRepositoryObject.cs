namespace Backups.Interfaces;

public interface IRepositoryObject
{
    void Accept(IRepositoryObjectVisitor repositoryObjectVisitor);
}