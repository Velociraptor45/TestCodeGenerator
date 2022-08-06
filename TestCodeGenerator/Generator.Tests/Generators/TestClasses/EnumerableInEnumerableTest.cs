namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class EnumerableInEnumerableTest
{
    public EnumerableInEnumerableTest(IEnumerable<IEnumerable<int>> ids)
    {
        Ids = ids;
    }

    public IEnumerable<IEnumerable<int>> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class EnumerableInEnumerableTestBuilder : DomainTestBuilderBase<EnumerableInEnumerableTest>
{
    public EnumerableInEnumerableTestBuilder WithIds(IEnumerable<IEnumerable<int>> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public EnumerableInEnumerableTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<IEnumerable<int>>());
    }
}
";
    }
}