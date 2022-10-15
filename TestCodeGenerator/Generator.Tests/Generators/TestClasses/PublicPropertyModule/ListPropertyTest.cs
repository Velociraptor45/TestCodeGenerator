namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class ListPropertyTest
{
    public List<int> Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
public class ListPropertyTestBuilder : DomainTestBuilderBase<ListPropertyTest>
{
    public ListPropertyTestBuilder WithIds(List<int> ids)
    {
        FillPropertyWith(p => p.Ids, ids);
        return this;
    }

    public ListPropertyTestBuilder WithEmptyIds()
    {
        return WithIds(new List<int>());
    }
}";
    }
}