using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class RestoreVisitor : IRepositoryObjectVisitor
{
    public RestoreVisitor(IRepository restoreRepository, IRepository oldRepository)
    {
        RestoreRepository = restoreRepository;
        OldRepository = oldRepository;
    }

    public IRepository RestoreRepository { get; }
    public IRepository OldRepository { get; }

    public void Visit(IFile file)
    {
        string relativePath = Path.GetRelativePath(OldRepository.PathToRepository, file.RepObjPath);
        string newPath = Path.Combine(RestoreRepository.PathToRepository, relativePath);
        using Stream restoreStream = RestoreRepository.OpenWrite(newPath); // TODO: union repPath + relative file path
        using Stream dataToRestore = file.GetStream();

        dataToRestore.CopyTo(restoreStream);
    }

    public void Visit(IFolder folder)
    {
        string relativePath = Path.GetRelativePath(OldRepository.PathToRepository, folder.RepObjPath);
        string newPath = Path.Combine(RestoreRepository.PathToRepository, relativePath);
        RestoreRepository.CreateDirectory(newPath);
        foreach (IRepositoryObject repositoryObject in folder.GetRepositoryObjects())
        {
            repositoryObject.Accept(this);
        }
    }
}