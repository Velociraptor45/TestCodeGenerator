namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class NullableDictionaryParameterTest
{
    public NullableDictionaryParameterTest(Dictionary<int, string>? ids)
    {
        Ids = ids;
    }

    public Dictionary<int, string>? Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
public class NullableDictionaryParameterTestBuilder : DomainTestBuilderBase<NullableDictionaryParameterTest>
{
    public NullableDictionaryParameterTestBuilder WithIds(Dictionary<int, string>? ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public NullableDictionaryParameterTestBuilder WithEmptyIds()
    {
        return WithIds(new Dictionary<int, string>());
    }

    public NullableDictionaryParameterTestBuilder WithoutIds()
    {
        return WithIds(null);
    }
}";
    }
}