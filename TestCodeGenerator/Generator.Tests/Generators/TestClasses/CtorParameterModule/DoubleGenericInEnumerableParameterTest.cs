using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class DoubleGenericInEnumerableParameterTest
{
    public DoubleGenericInEnumerableParameterTest(IEnumerable<DoubleGeneric<char, long>> ids)
    {
        Ids = ids;
    }

    public IEnumerable<DoubleGeneric<char, long>> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
public class DoubleGenericInEnumerableParameterTestBuilder : DomainTestBuilderBase<DoubleGenericInEnumerableParameterTest>
{
    public DoubleGenericInEnumerableParameterTestBuilder WithIds(IEnumerable<DoubleGeneric<char, long>> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public DoubleGenericInEnumerableParameterTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<DoubleGeneric<char, long>>());
    }
}";
    }
}