namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class ICollectionParameterTest
{
    public ICollectionParameterTest(ICollection<int> ids)
    {
        Ids = ids;
    }

    public ICollection<int> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class ICollectionParameterTestBuilder : DomainTestBuilderBase<ICollectionParameterTest>
               {
                   public ICollectionParameterTestBuilder WithIds(ICollection<int> ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public ICollectionParameterTestBuilder WithEmptyIds()
                   {
                       return WithIds(new List<int>());
                   }
               }
               """;
    }
}