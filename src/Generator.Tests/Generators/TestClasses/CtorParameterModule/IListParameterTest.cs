namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class IListParameterTest
{
    public IListParameterTest(IList<int> ids)
    {
        Ids = ids;
    }

    public IList<int> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class IListParameterTestBuilder : DomainTestBuilderBase<IListParameterTest>
               {
                   public IListParameterTestBuilder WithIds(IList<int> ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public IListParameterTestBuilder WithEmptyIds()
                   {
                       return WithIds(new List<int>());
                   }
               }
               """;
    }
}