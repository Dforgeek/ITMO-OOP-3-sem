using Backups.Interfaces;

namespace Backups.Extra.Entities;

public class RestoreVisitor : IRepositoryObjectVisitor
{
    public void Visit(IFile file)
    {
        file.
    }

    public void Visit(IFolder folder)
    {
        throw new NotImplementedException();
    }
}