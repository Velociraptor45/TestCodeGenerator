namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class ListTest
{
    public ListTest(List<int> ids)
    {
        Ids = ids;
    }

    public List<int> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;
public class ListTestBuilder : DomainTestBuilderBase<ListTest>
{
    public ListTestBuilder WithIds(List<int> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public ListTestBuilder WithEmptyIds()
    {
        return WithIds(new List<int>());
    }
}";
    }
}