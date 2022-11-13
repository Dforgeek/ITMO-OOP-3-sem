using Backups.Interfaces;

namespace Backups.Models;

public class BackupObject
{
    public BackupObject(IRepository repository, string pathFromRepToObject, Guid id)
    {
        if (repository.ValidatePathInsideRepository(pathFromRepToObject))
            throw new Exception();
        if (string.IsNullOrWhiteSpace(pathFromRepToObject))
            throw new Exception();
        PathFromRepToObject = pathFromRepToObject;
        Id = id;
        Repository = repository;
    }

    public Guid Id { get; }
    public string PathFromRepToObject { get; }
    public IRepository Repository { get; }

    public IRepositoryObject GetRepositoryObject()
    {
        return Repository.GetRepositoryObject(this); // Is it correct?
    }
}