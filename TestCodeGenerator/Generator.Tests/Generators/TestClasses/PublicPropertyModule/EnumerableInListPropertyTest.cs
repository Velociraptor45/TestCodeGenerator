namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class EnumerableInListPropertyTest
{
    public List<IEnumerable<int>> Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
public class EnumerableInListPropertyTestBuilder : DomainTestBuilderBase<EnumerableInListPropertyTest>
{
    public EnumerableInListPropertyTestBuilder WithIds(List<IEnumerable<int>> ids)
    {
        FillPropertyWith(p => p.Ids, ids);
        return this;
    }

    public EnumerableInListPropertyTestBuilder WithEmptyIds()
    {
        return WithIds(new List<IEnumerable<int>>());
    }
}";
    }
}