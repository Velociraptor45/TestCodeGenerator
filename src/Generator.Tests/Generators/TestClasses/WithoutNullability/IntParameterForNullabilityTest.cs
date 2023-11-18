namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.WithoutNullability;

public class IntParameterForNullabilityTest
{
    public IntParameterForNullabilityTest(int id)
    {
        Id = id;
    }

    public int Id { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.WithoutNullability;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.WithoutNullability;
               public class IntParameterForNullabilityTestBuilder : DomainTestBuilderBase<IntParameterForNullabilityTest>
               {
                   public IntParameterForNullabilityTestBuilder WithId(int id)
                   {
                       FillConstructorWith(nameof(id), id);
                       return this;
                   }
               }
               """;
    }
}