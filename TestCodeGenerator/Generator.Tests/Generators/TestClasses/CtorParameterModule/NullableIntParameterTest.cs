namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class NullableIntParameterTest
{
    public NullableIntParameterTest(int? id)
    {
        Id = id;
    }

    public int? Id { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
public class NullableIntParameterTestBuilder : DomainTestBuilderBase<NullableIntParameterTest>
{
    public NullableIntParameterTestBuilder WithId(int? id)
    {
        FillConstructorWith(nameof(id), id);
        return this;
    }

    public NullableIntParameterTestBuilder WithoutId()
    {
        return WithId(null);
    }
}";
    }
}