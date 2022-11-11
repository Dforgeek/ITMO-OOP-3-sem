using Backups.Interfaces;

namespace Backups.Entities;

public class ZipVisitor : IRepositoryObjectVisitor
{
    public void VisitFile(IFile file)
    {
        throw new NotImplementedException();
    }

    public void VisitFolder(IFolder folder)
    {
        throw new NotImplementedException();
    }
}