namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class NullableEnumerableWithNullableArgTest
{
    public NullableEnumerableWithNullableArgTest(IEnumerable<int?>? ids)
    {
        Ids = ids;
    }

    public IEnumerable<int?>? Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class NullableEnumerableWithNullableArgTestBuilder : DomainTestBuilderBase<NullableEnumerableWithNullableArgTest>
{
    public NullableEnumerableWithNullableArgTestBuilder WithIds(IEnumerable<int?>? ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public NullableEnumerableWithNullableArgTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<int?>());
    }

    public NullableEnumerableWithNullableArgTestBuilder WithoutIds()
    {
        return WithIds(null);
    }
}
";
    }
}