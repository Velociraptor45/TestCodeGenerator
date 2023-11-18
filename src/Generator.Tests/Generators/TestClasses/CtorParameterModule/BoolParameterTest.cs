namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class BoolParameterTest
{
    public BoolParameterTest(bool id)
    {
        Id = id;
    }

    public bool Id { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class BoolParameterTestBuilder : DomainTestBuilderBase<BoolParameterTest>
               {
                   public BoolParameterTestBuilder WithId(bool id)
                   {
                       FillConstructorWith(nameof(id), id);
                       return this;
                   }
               }
               """;
    }
}