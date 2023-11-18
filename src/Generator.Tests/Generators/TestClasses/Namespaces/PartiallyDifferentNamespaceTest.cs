namespace TestCodeGenerator.MyNamespace.Folder;

public class PartiallyDifferentNamespaceTest
{
    public PartiallyDifferentNamespaceTest(bool id)
    {
        Id = id;
    }

    public bool Id { get; }

    public static string GetExpectedBuilder()
    {
        return """
                using Superclass.Namespace;
                using System;
                using TestCodeGenerator.MyNamespace.Folder;

                namespace TestCodeGenerator.Generator.Tests.Tests.MyNamespace.Folder;
                public class PartiallyDifferentNamespaceTestBuilder : DomainTestBuilderBase<PartiallyDifferentNamespaceTest>
                {
                    public PartiallyDifferentNamespaceTestBuilder WithId(bool id)
                    {
                        FillConstructorWith(nameof(id), id);
                        return this;
                    }
                }
                """;
    }
}