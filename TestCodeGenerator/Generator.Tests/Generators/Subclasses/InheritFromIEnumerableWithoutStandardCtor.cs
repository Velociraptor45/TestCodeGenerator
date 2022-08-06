using System.Collections;

namespace TestCodeGenerator.Generator.Tests.Generators.Subclasses;

public class InheritFromIEnumerableWithoutStandardCtor : IEnumerable<float>
{
    private readonly List<float> _floats;

    public InheritFromIEnumerableWithoutStandardCtor(IEnumerable<float> floats)
    {
        _floats = floats.ToList();
    }

    public IEnumerator<float> GetEnumerator()
    {
        return _floats.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}