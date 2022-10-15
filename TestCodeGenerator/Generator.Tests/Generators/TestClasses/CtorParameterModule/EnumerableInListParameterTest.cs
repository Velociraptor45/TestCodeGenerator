namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class EnumerableInListParameterTest
{
    public EnumerableInListParameterTest(List<IEnumerable<int>> ids)
    {
        Ids = ids;
    }

    public List<IEnumerable<int>> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
public class EnumerableInListParameterTestBuilder : DomainTestBuilderBase<EnumerableInListParameterTest>
{
    public EnumerableInListParameterTestBuilder WithIds(List<IEnumerable<int>> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public EnumerableInListParameterTestBuilder WithEmptyIds()
    {
        return WithIds(new List<IEnumerable<int>>());
    }
}";
    }
}