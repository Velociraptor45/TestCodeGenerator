using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class DoubleGenericInEnumerablePropertyTest
{
    public IEnumerable<DoubleGeneric<char, long>> Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
public class DoubleGenericInEnumerablePropertyTestBuilder : DomainTestBuilderBase<DoubleGenericInEnumerablePropertyTest>
{
    public DoubleGenericInEnumerablePropertyTestBuilder WithIds(IEnumerable<DoubleGeneric<char, long>> ids)
    {
        FillPropertyWith(p => p.Ids, ids);
        return this;
    }

    public DoubleGenericInEnumerablePropertyTestBuilder WithEmptyIds()
    {
        return WithIds(Enumerable.Empty<DoubleGeneric<char, long>>());
    }
}";
    }
}