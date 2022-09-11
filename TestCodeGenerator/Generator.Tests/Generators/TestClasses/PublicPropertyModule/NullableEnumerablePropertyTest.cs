namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class NullableEnumerablePropertyTest
{
    public IEnumerable<int>? Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
public class NullableEnumerablePropertyTestBuilder : DomainTestBuilderBase<NullableEnumerablePropertyTest>
{
    public NullableEnumerablePropertyTestBuilder WithIds(IEnumerable<int>? ids)
    {
        FillPropertyWith(p => p.Ids, ids);
        return this;
    }

    public NullableEnumerablePropertyTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<int>());
    }

    public NullableEnumerablePropertyTestBuilder WithoutIds()
    {
        return WithIds(null);
    }
}";
    }
}