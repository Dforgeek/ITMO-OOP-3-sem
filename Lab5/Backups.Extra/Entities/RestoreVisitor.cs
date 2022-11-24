using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class RestoreVisitor : IRepositoryObjectVisitor
{
    public RestoreVisitor(IRepository restoreRepository)
    {
        RestoreRepository = restoreRepository;
    }

    public IRepository RestoreRepository { get; }

    public void Visit(IFile file)
    {
        using Stream restoreStream = RestoreRepository.OpenWrite(file.RepObjPath);
        using Stream dataToRestore = file.GetStream();

        dataToRestore.CopyTo(restoreStream);
    }

    public void Visit(IFolder folder)
    {
        foreach (IRepositoryObject repositoryObject in folder.GetRepositoryObjects())
        {
            repositoryObject.Accept(this);
        }
    }
}