using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra.Services;

public class ToDifferLocationRestoreService : IRestoreService
{
    public ToDifferLocationRestoreService(IRepository differRepository)
    {
        DifferRepository = differRepository;
    }

    public IRepository DifferRepository { get; }

    public void Restore(RestorePoint restorePoint)
    {
        IReadOnlyCollection<IRepositoryObject> repositoryObjects =
            restorePoint.Storage.GetWrapper().GetRepositoryObjects();
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            BackupObject backupObject = restorePoint.BackupObjects
                .FirstOrDefault(bo => bo.Path == repositoryObject.RepObjPath) ?? throw new Exception();
            var restoreVisitor = new RestoreVisitor(DifferRepository, backupObject.Repository);
            repositoryObject.Accept(restoreVisitor);
        }
    }
}