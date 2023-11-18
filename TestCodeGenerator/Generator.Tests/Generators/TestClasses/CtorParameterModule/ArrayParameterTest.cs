namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class ArrayParameterTest
{
    public ArrayParameterTest(int[] ids)
    {
        Ids = ids;
        Array.Empty<int>();
    }

    public int[] Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class ArrayParameterTestBuilder : DomainTestBuilderBase<ArrayParameterTest>
               {
                   public ArrayParameterTestBuilder WithIds(int[] ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public ArrayParameterTestBuilder WithEmptyIds()
                   {
                       return WithIds(Array.Empty<int>());
                   }
               }
               """;
    }
}