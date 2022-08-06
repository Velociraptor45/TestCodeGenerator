namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class BoolTest
{
    public BoolTest(bool id)
    {
        Id = id;
    }

    public bool Id { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class BoolTestBuilder : DomainTestBuilderBase<BoolTest>
{
    public BoolTestBuilder WithId(bool id)
    {
        FillConstructorWith(nameof(id), id);
        return this;
    }
}
";
    }
}