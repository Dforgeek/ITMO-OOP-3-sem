using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public class ElectiveModule
{
    private readonly List<Stream> _streams;

    private ElectiveModule(Guid id, string name, MegaFacultyPrefix prefix, List<Stream> streams)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();
        Name = name;
        Id = id;
        MegaFacultyPrefix = prefix;
        _streams = streams;
    }

    public Guid Id { get; }
    public string Name { get; }
    public MegaFacultyPrefix MegaFacultyPrefix { get; }

    public IReadOnlyList<Stream> Streams => _streams.AsReadOnly();
}