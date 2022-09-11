using System.Collections;

namespace TestCodeGenerator.Generator.Models;

public class Namespaces : IEnumerable<string>
{
    private readonly HashSet<string> _namespaces = new();

    public void Add(string nmsp)
    {
        if (!_namespaces.Contains(nmsp))
            _namespaces.Add(nmsp);
    }

    public void AddRange(IEnumerable<string> namespaces)
    {
        foreach (var nmsp in namespaces)
        {
            Add(nmsp);
        }
    }

    public IEnumerator<string> GetEnumerator()
    {
        return _namespaces.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}