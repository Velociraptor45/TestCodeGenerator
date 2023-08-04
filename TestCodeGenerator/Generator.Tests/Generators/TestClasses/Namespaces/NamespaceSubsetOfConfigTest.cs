namespace TestCodeGenerator.Generator.Tests;

public class NamespaceSubsetOfConfigTest
{
    public NamespaceSubsetOfConfigTest(bool id)
    {
        Id = id;
    }

    public bool Id { get; }

    public static string GetExpectedBuilder()
    {
        return """
                using Superclass.Namespace;
                using System;
                using TestCodeGenerator.Generator.Tests;

                namespace TestCodeGenerator.Generator.Tests.Tests;
                public class NamespaceSubsetOfConfigTestBuilder : DomainTestBuilderBase<NamespaceSubsetOfConfigTest>
                {
                    public NamespaceSubsetOfConfigTestBuilder WithId(bool id)
                    {
                        FillConstructorWith(nameof(id), id);
                        return this;
                    }
                }
                """;
    }
}