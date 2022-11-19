using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.WithoutNullability;

public class StructParameterTest
{
    public StructParameterTest(StructDummy id)
    {
        Id = id;
    }

    public StructDummy Id { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.WithoutNullability;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.WithoutNullability;
public class StructParameterTestBuilder : DomainTestBuilderBase<StructParameterTest>
{
    public StructParameterTestBuilder WithId(StructDummy id)
    {
        FillConstructorWith(nameof(id), id);
        return this;
    }
}";
    }
}