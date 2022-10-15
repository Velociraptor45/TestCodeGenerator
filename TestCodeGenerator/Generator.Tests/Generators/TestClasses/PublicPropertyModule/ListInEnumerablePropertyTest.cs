namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class ListInEnumerablePropertyTest
{
    public IEnumerable<List<int>?> Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
public class ListInEnumerablePropertyTestBuilder : DomainTestBuilderBase<ListInEnumerablePropertyTest>
{
    public ListInEnumerablePropertyTestBuilder WithIds(IEnumerable<List<int>?> ids)
    {
        FillPropertyWith(p => p.Ids, ids);
        return this;
    }

    public ListInEnumerablePropertyTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<List<int>?>());
    }
}";
    }
}