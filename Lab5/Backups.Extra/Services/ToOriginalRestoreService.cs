using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class ToOriginalRestoreService : IRestoreService
{
    private readonly RestoreVisitor _restoreVisitor;

    public ToOriginalRestoreService(IRepository repository)
    {
        _restoreVisitor = new RestoreVisitor(repository);
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