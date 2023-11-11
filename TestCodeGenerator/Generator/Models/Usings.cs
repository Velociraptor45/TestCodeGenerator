using RefleCS.Nodes;
using System.Collections;

namespace TestCodeGenerator.Generator.Models;

public class Usings : IEnumerable<Using>
{
    private readonly HashSet<Using> _namespaces = new();

    public void Add(string usng)
    {
        Add(new Using(usng));
    }

    public void Add(Using usng)
    {
        _namespaces.Add(usng);
    }

    public void AddRange(IEnumerable<string> namespaces)
    {
        foreach (var nmsp in namespaces)
        {
            Add(nmsp);
        }
    }

    public IEnumerator<Using> GetEnumerator()
    {
        return _namespaces.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}