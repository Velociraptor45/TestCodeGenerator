namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class NullableDictionaryPropertyTest
{
    public Dictionary<int, string>? Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
public class NullableDictionaryPropertyTestBuilder : DomainTestBuilderBase<NullableDictionaryPropertyTest>
{
    public NullableDictionaryPropertyTestBuilder WithIds(Dictionary<int, string>? ids)
    {
        FillPropertyWith(p => p.Ids, ids);
        return this;
    }

    public NullableDictionaryPropertyTestBuilder WithEmptyIds()
    {
        return WithIds(new Dictionary<int, string>());
    }

    public NullableDictionaryPropertyTestBuilder WithoutIds()
    {
        return WithIds(null);
    }
}";
    }
}