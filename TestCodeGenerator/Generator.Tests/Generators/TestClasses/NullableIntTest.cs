namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class NullableIntTest
{
    public NullableIntTest(int? id)
    {
        Id = id;
    }

    public int? Id { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class NullableIntTestBuilder : DomainTestBuilderBase<NullableIntTest>
{
    public NullableIntTestBuilder WithId(int? id)
    {
        FillConstructorWith(nameof(id), id);
        return this;
    }

    public NullableIntTestBuilder WithoutId()
    {
        return WithId(null);
    }
}
";
    }
}