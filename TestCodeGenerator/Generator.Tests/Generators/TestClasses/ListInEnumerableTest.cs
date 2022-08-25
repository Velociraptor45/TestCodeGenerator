namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class ListInEnumerableTest
{
    public ListInEnumerableTest(IEnumerable<List<int>?> ids)
    {
        Ids = ids;
    }

    public IEnumerable<List<int>?> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;
public class ListInEnumerableTestBuilder : DomainTestBuilderBase<ListInEnumerableTest>
{
    public ListInEnumerableTestBuilder WithIds(IEnumerable<List<int>?> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public ListInEnumerableTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<List<int>?>());
    }
}";
    }
}