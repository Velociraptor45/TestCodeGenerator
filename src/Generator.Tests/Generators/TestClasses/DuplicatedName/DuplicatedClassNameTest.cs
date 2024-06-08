namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.DuplicatedName;

public class DuplicatedClassNameTest
{
    public DuplicatedClassNameTest(bool id)
    {
        Id = id;
    }

    public bool Id { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.DuplicatedName;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.DuplicatedName;
               public class DuplicatedClassNameTestBuilder : DomainTestBuilderBase<DuplicatedClassNameTest>
               {
                   public DuplicatedClassNameTestBuilder WithId(bool id)
                   {
                       FillConstructorWith(nameof(id), id);
                       return this;
                   }
               }
               """;
    }
}