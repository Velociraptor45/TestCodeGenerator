using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class SingleGenericInDoubleGenericParameterTest
{
    public SingleGenericInDoubleGenericParameterTest(DoubleGeneric<SingleGeneric<short>, decimal> ids)
    {
        Ids = ids;
    }

    public DoubleGeneric<SingleGeneric<short>, decimal> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class SingleGenericInDoubleGenericParameterTestBuilder : DomainTestBuilderBase<SingleGenericInDoubleGenericParameterTest>
               {
                   public SingleGenericInDoubleGenericParameterTestBuilder WithIds(DoubleGeneric<SingleGeneric<short>, decimal> ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }
               }
               """;
    }
}