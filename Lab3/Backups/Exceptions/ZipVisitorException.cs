namespace Backups.Exceptions;

public class ZipVisitorException : Exception
{
    private ZipVisitorException() { }

    public static ZipVisitorException MoreThanOneLayerOfZipObjectList_InvariantCorrupted()
    {
        return new ZipVisitorException();
    }
}