namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class NullableDictionaryWithSecondArgNullableTest
{
    public NullableDictionaryWithSecondArgNullableTest(Dictionary<int, string?>? ids)
    {
        Ids = ids;
    }

    public Dictionary<int, string?>? Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;
public class NullableDictionaryWithSecondArgNullableTestBuilder : DomainTestBuilderBase<NullableDictionaryWithSecondArgNullableTest>
{
    public NullableDictionaryWithSecondArgNullableTestBuilder WithIds(Dictionary<int, string?>? ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public NullableDictionaryWithSecondArgNullableTestBuilder WithEmptyIds()
    {
        return WithIds(new Dictionary<int, string?>());
    }

    public NullableDictionaryWithSecondArgNullableTestBuilder WithoutIds()
    {
        return WithIds(null);
    }
}";
    }
}