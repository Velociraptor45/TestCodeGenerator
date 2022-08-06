namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class NullableEnumerableTest
{
    public NullableEnumerableTest(IEnumerable<int>? ids)
    {
        Ids = ids;
    }

    public IEnumerable<int>? Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class NullableEnumerableTestBuilder : DomainTestBuilderBase<NullableEnumerableTest>
{
    public NullableEnumerableTestBuilder WithIds(IEnumerable<int>? ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public NullableEnumerableTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<int>());
    }

    public NullableEnumerableTestBuilder WithoutIds()
    {
        return WithIds(null);
    }
}
";
    }
}