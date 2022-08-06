using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class DoubleGenericTest
{
    public DoubleGenericTest(DoubleGeneric<char, decimal> ids)
    {
        Ids = ids;
    }

    public DoubleGeneric<char, decimal> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class DoubleGenericTestBuilder : DomainTestBuilderBase<DoubleGenericTest>
{
    public DoubleGenericTestBuilder WithIds(DoubleGeneric<char, decimal> ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }
}
";
    }
}