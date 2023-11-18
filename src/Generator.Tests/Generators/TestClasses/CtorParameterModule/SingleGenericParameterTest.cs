using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class SingleGenericParameterTest
{
    public SingleGenericParameterTest(SingleGeneric<char> ids)
    {
        Ids = ids;
    }

    public SingleGeneric<char> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class SingleGenericParameterTestBuilder : DomainTestBuilderBase<SingleGenericParameterTest>
               {
                   public SingleGenericParameterTestBuilder WithIds(SingleGeneric<char> ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }
               }
               """;
    }
}