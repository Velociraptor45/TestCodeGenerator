namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class EnumerableTest
{
    public EnumerableTest(IEnumerable<int> ids)
    {
        Ids = ids;
    }

    public IEnumerable<int> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class EnumerableTestBuilder : DomainTestBuilderBase<EnumerableTest>
{
    public EnumerableTestBuilder WithIds(IEnumerable<int> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public EnumerableTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<int>());
    }
}
";
    }
}