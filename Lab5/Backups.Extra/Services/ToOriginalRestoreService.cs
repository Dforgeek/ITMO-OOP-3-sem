using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Services;

public class ToOriginalRestoreService : IRestoreService
{
    public void Restore(RestorePoint restorePoint)
    {
        IReadOnlyCollection<IRepositoryObject> repositoryObjects =
            restorePoint.Storage.GetWrapper().GetRepositoryObjects();

        var restoreVisitor = new RestoreVisitor(restorePoint.Storage.Repository, restorePoint.Storage.Repository);

        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            repositoryObject.Accept(restoreVisitor);
        }
    }
}