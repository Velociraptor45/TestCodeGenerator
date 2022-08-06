using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class SingleGenericInDoubleGenericTest
{
    public SingleGenericInDoubleGenericTest(DoubleGeneric<SingleGeneric<short>, decimal> ids)
    {
        Ids = ids;
    }

    public DoubleGeneric<SingleGeneric<short>, decimal> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class SingleGenericInDoubleGenericTestBuilder : DomainTestBuilderBase<SingleGenericInDoubleGenericTest>
{
    public SingleGenericInDoubleGenericTestBuilder WithIds(DoubleGeneric<SingleGeneric<short>, decimal> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }
}
";
    }
}