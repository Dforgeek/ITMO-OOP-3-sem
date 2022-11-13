using File = Backups.Entities.File;

namespace Backups.Interfaces;

public interface IRepositoryObjectVisitor
{
    void Visit(IFile file);

    void Visit(IFolder folder);
}