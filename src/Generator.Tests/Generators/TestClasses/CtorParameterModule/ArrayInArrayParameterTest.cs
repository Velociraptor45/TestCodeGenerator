namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class ArrayInArrayParameterTest
{
    public ArrayInArrayParameterTest(int[][] ids)
    {
        Ids = ids;
        Array.Empty<int>();
    }

    public int[][] Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class ArrayInArrayParameterTestBuilder : DomainTestBuilderBase<ArrayInArrayParameterTest>
               {
                   public ArrayInArrayParameterTestBuilder WithIds(int[][] ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public ArrayInArrayParameterTestBuilder WithEmptyIds()
                   {
                       return WithIds(Array.Empty<int[]>());
                   }
               }
               """;
    }
}