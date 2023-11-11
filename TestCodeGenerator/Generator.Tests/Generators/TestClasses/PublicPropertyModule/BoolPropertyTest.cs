namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class BoolPropertyTest
{
    public bool Id { get; set; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
               public class BoolPropertyTestBuilder : DomainTestBuilderBase<BoolPropertyTest>
               {
                   public BoolPropertyTestBuilder WithId(bool id)
                   {
                       FillPropertyWith(p => p.Id, id);
                       return this;
                   }
               }
               """;
    }
}