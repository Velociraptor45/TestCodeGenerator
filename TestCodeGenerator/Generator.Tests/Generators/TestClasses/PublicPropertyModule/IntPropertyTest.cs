namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class IntPropertyTest
{
    public int Id { get; set; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
public class IntPropertyTestBuilder : DomainTestBuilderBase<IntPropertyTest>
{
    public IntPropertyTestBuilder WithId(int id)
    {
        FillPropertyWith(p => p.Id, id);
        return this;
    }
}";
    }
}