namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class EnumerableInListTest
{
    public EnumerableInListTest(List<IEnumerable<int>> ids)
    {
        Ids = ids;
    }

    public List<IEnumerable<int>> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;
public class EnumerableInListTestBuilder : DomainTestBuilderBase<EnumerableInListTest>
{
    public EnumerableInListTestBuilder WithIds(List<IEnumerable<int>> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public EnumerableInListTestBuilder WithEmptyIds()
    {
        return WithIds(new List<IEnumerable<int>>());
    }
}";
    }
}