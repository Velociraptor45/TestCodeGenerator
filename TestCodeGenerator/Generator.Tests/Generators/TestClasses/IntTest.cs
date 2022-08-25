namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class IntTest
{
    public IntTest(int id)
    {
        Id = id;
    }

    public int Id { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;
public class IntTestBuilder : DomainTestBuilderBase<IntTest>
{
    public IntTestBuilder WithId(int id)
    {
        FillConstructorWith(nameof(id), id);
        return this;
    }
}";
    }
}