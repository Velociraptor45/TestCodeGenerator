namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.WithoutNullability;

public class NullableIntParameterForNullabilityTest
{
    public NullableIntParameterForNullabilityTest(int? id)
    {
        Id = id;
    }

    public int? Id { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.WithoutNullability;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.WithoutNullability;
public class NullableIntParameterForNullabilityTestBuilder : DomainTestBuilderBase<NullableIntParameterForNullabilityTest>
{
    public NullableIntParameterForNullabilityTestBuilder WithId(int? id)
    {
        FillConstructorWith(nameof(id), id);
        return this;
    }

    public NullableIntParameterForNullabilityTestBuilder WithoutId()
    {
        return WithId(null);
    }
}";
    }
}