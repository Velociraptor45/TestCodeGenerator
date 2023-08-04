namespace TestCodeGenerator.Generator.Tests.Tests;

public class NamespaceSameAsConfigTest
{
    public NamespaceSameAsConfigTest(bool id)
    {
        Id = id;
    }

    public bool Id { get; }

    public static string GetExpectedBuilder()
    {
        return """
                using Superclass.Namespace;
                using System;
                using TestCodeGenerator.Generator.Tests.Tests;

                namespace TestCodeGenerator.Generator.Tests.Tests;
                public class NamespaceSameAsConfigTestBuilder : DomainTestBuilderBase<NamespaceSameAsConfigTest>
                {
                    public NamespaceSameAsConfigTestBuilder WithId(bool id)
                    {
                        FillConstructorWith(nameof(id), id);
                        return this;
                    }
                }
                """;
    }
}