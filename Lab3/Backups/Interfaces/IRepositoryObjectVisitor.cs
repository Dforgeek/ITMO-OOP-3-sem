using File = Backups.Entities.File;

namespace Backups.Interfaces;

public interface IRepositoryObjectVisitor
{
    void VisitFile(IFile file);

    void VisitFolder(IFolder folder);
}