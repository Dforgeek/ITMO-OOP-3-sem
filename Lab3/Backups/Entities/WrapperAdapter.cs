using System.IO.Compression;
using System.Runtime.InteropServices;
using Backups.Interfaces;
using Zio;

namespace Backups.Entities;

public class WrapperAdapter : IWrapper
{
    private IReadOnlyCollection<IWrapper> _wrappers;

    public WrapperAdapter(IReadOnlyCollection<IWrapper> wrappers)
    {
        _wrappers = wrappers;
    }

    public IReadOnlyCollection<IZipObject> ZipObjects
    {
        get
        {
            var zipObjects = new List<IZipObject>();
            foreach (IWrapper wrapper in _wrappers)
            {
                zipObjects.AddRange(wrapper.ZipObjects);
            }

            return zipObjects;
        }
    }

    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        var repositoryObjects = new List<IRepositoryObject>();
        foreach (IWrapper wrapper in _wrappers)
        {
            repositoryObjects.AddRange(wrapper.GetRepositoryObjects());
        }

        return repositoryObjects;
    }

    public void Dispose()
    {
        foreach (IWrapper wrapper in _wrappers)
        {
            wrapper.Dispose();
        }
    }
}