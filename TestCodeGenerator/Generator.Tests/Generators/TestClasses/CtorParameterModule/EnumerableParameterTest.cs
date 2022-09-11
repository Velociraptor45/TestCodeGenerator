namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class EnumerableParameterTest
{
    public EnumerableParameterTest(IEnumerable<int> ids)
    {
        Ids = ids;
    }

    public IEnumerable<int> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
public class EnumerableParameterTestBuilder : DomainTestBuilderBase<EnumerableParameterTest>
{
    public EnumerableParameterTestBuilder WithIds(IEnumerable<int> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public EnumerableParameterTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<int>());
    }
}";
    }
}