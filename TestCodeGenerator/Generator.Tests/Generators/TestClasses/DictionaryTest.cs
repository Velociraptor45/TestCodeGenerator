namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class DictionaryTest
{
    public DictionaryTest(Dictionary<int, string> ids)
    {
        Ids = ids;
    }

    public Dictionary<int, string> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using System.Collections.Generic;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class DictionaryTestBuilder : DomainTestBuilderBase<DictionaryTest>
{
    public DictionaryTestBuilder WithIds(Dictionary<int, string> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public DictionaryTestBuilder WithEmptyIds()
    {
        return WithIds(new Dictionary<int, string>());
    }
}
";
    }
}