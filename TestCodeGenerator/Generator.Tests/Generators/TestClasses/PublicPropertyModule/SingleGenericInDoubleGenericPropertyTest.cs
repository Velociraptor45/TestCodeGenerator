using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class SingleGenericInDoubleGenericPropertyTest
{
    public DoubleGeneric<SingleGeneric<short>, decimal> Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
               public class SingleGenericInDoubleGenericPropertyTestBuilder : DomainTestBuilderBase<SingleGenericInDoubleGenericPropertyTest>
               {
                   public SingleGenericInDoubleGenericPropertyTestBuilder WithIds(DoubleGeneric<SingleGeneric<short>, decimal> ids)
                   {
                       FillPropertyWith(p => p.Ids, ids);
                       return this;
                   }
               }
               """;
    }
}