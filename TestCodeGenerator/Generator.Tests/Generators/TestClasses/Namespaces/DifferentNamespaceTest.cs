namespace MyNamespace.Folder;

public class DifferentNamespaceTest
{
    public DifferentNamespaceTest(bool id)
    {
        Id = id;
    }

    public bool Id { get; }

    public static string GetExpectedBuilder()
    {
        return """
                using MyNamespace.Folder;
                using Superclass.Namespace;
                using System;

                namespace TestCodeGenerator.Generator.Tests.Tests.MyNamespace.Folder;
                public class DifferentNamespaceTestBuilder : DomainTestBuilderBase<DifferentNamespaceTest>
                {
                    public DifferentNamespaceTestBuilder WithId(bool id)
                    {
                        FillConstructorWith(nameof(id), id);
                        return this;
                    }
                }
                """;
    }
}