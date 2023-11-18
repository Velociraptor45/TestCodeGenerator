using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class SingleGenericPropertyTest
{
    public SingleGeneric<char> Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
               public class SingleGenericPropertyTestBuilder : DomainTestBuilderBase<SingleGenericPropertyTest>
               {
                   public SingleGenericPropertyTestBuilder WithIds(SingleGeneric<char> ids)
                   {
                       FillPropertyWith(p => p.Ids, ids);
                       return this;
                   }
               }
               """;
    }
}