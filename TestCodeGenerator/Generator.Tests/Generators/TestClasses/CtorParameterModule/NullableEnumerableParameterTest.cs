namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class NullableEnumerableParameterTest
{
    public NullableEnumerableParameterTest(IEnumerable<int>? ids)
    {
        Ids = ids;
    }

    public IEnumerable<int>? Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
public class NullableEnumerableParameterTestBuilder : DomainTestBuilderBase<NullableEnumerableParameterTest>
{
    public NullableEnumerableParameterTestBuilder WithIds(IEnumerable<int>? ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public NullableEnumerableParameterTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<int>());
    }

    public NullableEnumerableParameterTestBuilder WithoutIds()
    {
        return WithIds(null);
    }
}";
    }
}