using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class RestoreService : IRestoreService
{
    private readonly RestoreVisitor _restoreVisitor;

    public RestoreService()
    {
        _restoreVisitor = new RestoreVisitor();
    }

    public void Restore(RestorePoint restorePoint)
    {
        IReadOnlyCollection<IRepositoryObject> repositoryObjects =
            restorePoint.Storage.GetWrapper().GetRepositoryObjects();
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            repositoryObject.Accept(_restoreVisitor);
        }
    }
}