using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class DoubleGenericInEnumerableTest
{
    public DoubleGenericInEnumerableTest(IEnumerable<DoubleGeneric<char, long>> ids)
    {
        Ids = ids;
    }

    public IEnumerable<DoubleGeneric<char, long>> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class DoubleGenericInEnumerableTestBuilder : DomainTestBuilderBase<DoubleGenericInEnumerableTest>
{
    public DoubleGenericInEnumerableTestBuilder WithIds(IEnumerable<DoubleGeneric<char, long>> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public DoubleGenericInEnumerableTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<DoubleGeneric<char, long>>());
    }
}
";
    }
}