using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class SingleGenericTest
{
    public SingleGenericTest(SingleGeneric<char> ids)
    {
        Ids = ids;
    }

    public SingleGeneric<char> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class SingleGenericTestBuilder : DomainTestBuilderBase<SingleGenericTest>
{
    public SingleGenericTestBuilder WithIds(SingleGeneric<char> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }
}
";
    }
}