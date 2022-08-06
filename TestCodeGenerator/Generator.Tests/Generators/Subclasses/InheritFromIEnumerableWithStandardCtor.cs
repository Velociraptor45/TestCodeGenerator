using System.Collections;

namespace TestCodeGenerator.Generator.Tests.Generators.Subclasses;

public class InheritFromIEnumerableWithStandardCtor : IEnumerable<float>
{
    private readonly List<float> _floats = new();

    public InheritFromIEnumerableWithStandardCtor()
    {
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