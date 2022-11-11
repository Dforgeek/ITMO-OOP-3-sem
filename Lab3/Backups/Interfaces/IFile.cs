namespace Backups.Interfaces;

public interface IFile : IRepositoryObject
{
    Stream GetStream();
}