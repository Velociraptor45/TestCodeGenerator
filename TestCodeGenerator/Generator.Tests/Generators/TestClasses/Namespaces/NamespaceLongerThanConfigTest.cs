namespace TestCodeGenerator.MyNamespace.Folder.Sub.Sub.Inside.Another.Folder.Here;

public class NamespaceLongerThanConfigTest
{
    public NamespaceLongerThanConfigTest(bool id)
    {
        Id = id;
    }

    public bool Id { get; }

    public static string GetExpectedBuilder()
    {
        return """
                using Superclass.Namespace;
                using System;
                using TestCodeGenerator.MyNamespace.Folder.Sub.Sub.Inside.Another.Folder.Here;

                namespace TestCodeGenerator.Generator.Tests.Tests.MyNamespace.Folder.Sub.Sub.Inside.Another.Folder.Here;
                public class NamespaceLongerThanConfigTestBuilder : DomainTestBuilderBase<NamespaceLongerThanConfigTest>
                {
                    public NamespaceLongerThanConfigTestBuilder WithId(bool id)
                    {
                        FillConstructorWith(nameof(id), id);
                        return this;
                    }
                }
                """;
    }
}