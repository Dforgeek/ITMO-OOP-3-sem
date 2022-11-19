using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class RestoreService
{
    private readonly RestoreVisitor _restoreVisitor;

    public RestoreService()
    {
        _restoreVisitor = new RestoreVisitor();
    }

    public void Restore(RestorePoint restorePoint)
    {
        IReadOnlyCollection<IRepositoryObject> repositoryObjects = restorePoint.Storage.GetRepositoryObjects();
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            repositoryObject.Accept(_restoreVisitor);
        }
    }
}