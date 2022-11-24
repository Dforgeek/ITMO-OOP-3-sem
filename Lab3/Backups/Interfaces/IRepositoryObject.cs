namespace Backups.Interfaces;

public interface IRepositoryObject
{
    string RepObjPath { get; }
    void Accept(IRepositoryObjectVisitor repositoryObjectVisitor);
}