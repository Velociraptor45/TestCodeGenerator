namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class NullableDictionaryTest
{
    public NullableDictionaryTest(Dictionary<int, string>? ids)
    {
        Ids = ids;
    }

    public Dictionary<int, string>? Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;
public class NullableDictionaryTestBuilder : DomainTestBuilderBase<NullableDictionaryTest>
{
    public NullableDictionaryTestBuilder WithIds(Dictionary<int, string>? ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public NullableDictionaryTestBuilder WithEmptyIds()
    {
        return WithIds(new Dictionary<int, string>());
    }

    public NullableDictionaryTestBuilder WithoutIds()
    {
        return WithIds(null);
    }
}";
    }
}