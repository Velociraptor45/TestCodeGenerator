namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class IntParameterTest
{
    public IntParameterTest(int id)
    {
        Id = id;
    }

    public int Id { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
public class IntParameterTestBuilder : DomainTestBuilderBase<IntParameterTest>
{
    public IntParameterTestBuilder WithId(int id)
    {
        FillConstructorWith(nameof(id), id);
        return this;
    }
}";
    }
}