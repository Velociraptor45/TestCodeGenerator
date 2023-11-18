using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class DoubleGenericParameterTest
{
    public DoubleGenericParameterTest(DoubleGeneric<char, decimal> ids)
    {
        Ids = ids;
    }

    public DoubleGeneric<char, decimal> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class DoubleGenericParameterTestBuilder : DomainTestBuilderBase<DoubleGenericParameterTest>
               {
                   public DoubleGenericParameterTestBuilder WithIds(DoubleGeneric<char, decimal> ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }
               }
               """;
    }
}