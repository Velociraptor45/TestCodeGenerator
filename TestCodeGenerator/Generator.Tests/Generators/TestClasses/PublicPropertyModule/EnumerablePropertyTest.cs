namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class EnumerablePropertyTest
{
    public IEnumerable<int> Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
public class EnumerablePropertyTestBuilder : DomainTestBuilderBase<EnumerablePropertyTest>
{
    public EnumerablePropertyTestBuilder WithIds(IEnumerable<int> ids)
    {
        FillPropertyWith(p => p.Ids, ids);
        return this;
    }

    public EnumerablePropertyTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<int>());
    }
}";
    }
}