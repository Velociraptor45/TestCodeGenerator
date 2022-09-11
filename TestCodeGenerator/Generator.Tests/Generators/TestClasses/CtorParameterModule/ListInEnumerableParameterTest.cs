namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class ListInEnumerableParameterTest
{
    public ListInEnumerableParameterTest(IEnumerable<List<int>?> ids)
    {
        Ids = ids;
    }

    public IEnumerable<List<int>?> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
public class ListInEnumerableParameterTestBuilder : DomainTestBuilderBase<ListInEnumerableParameterTest>
{
    public ListInEnumerableParameterTestBuilder WithIds(IEnumerable<List<int>?> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public ListInEnumerableParameterTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<List<int>?>());
    }
}";
    }
}