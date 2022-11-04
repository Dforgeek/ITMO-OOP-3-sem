namespace Backups.Interfaces;

public interface IArchiver
{
    void Encode(Stream streamIn, Stream streamOut, string fileName);
}