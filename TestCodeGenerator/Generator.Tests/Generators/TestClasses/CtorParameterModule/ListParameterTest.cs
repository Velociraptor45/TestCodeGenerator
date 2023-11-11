namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class ListParameterTest
{
    public ListParameterTest(List<int> ids)
    {
        Ids = ids;
    }

    public List<int> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class ListParameterTestBuilder : DomainTestBuilderBase<ListParameterTest>
               {
                   public ListParameterTestBuilder WithIds(List<int> ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public ListParameterTestBuilder WithEmptyIds()
                   {
                       return WithIds(new List<int>());
                   }
               }
               """;
    }
}