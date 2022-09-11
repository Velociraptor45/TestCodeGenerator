namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class EnumerableInEnumerableParameterTest
{
    public EnumerableInEnumerableParameterTest(IEnumerable<IEnumerable<int>> ids)
    {
        Ids = ids;
    }

    public IEnumerable<IEnumerable<int>> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
public class EnumerableInEnumerableParameterTestBuilder : DomainTestBuilderBase<EnumerableInEnumerableParameterTest>
{
    public EnumerableInEnumerableParameterTestBuilder WithIds(IEnumerable<IEnumerable<int>> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public EnumerableInEnumerableParameterTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<IEnumerable<int>>());
    }
}";
    }
}