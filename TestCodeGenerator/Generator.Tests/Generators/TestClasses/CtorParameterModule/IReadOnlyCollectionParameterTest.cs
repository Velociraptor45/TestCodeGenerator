namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class IReadOnlyCollectionParameterTest
{
    public IReadOnlyCollectionParameterTest(IReadOnlyCollection<int> ids)
    {
        Ids = ids;
    }

    public IReadOnlyCollection<int> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class IReadOnlyCollectionParameterTestBuilder : DomainTestBuilderBase<IReadOnlyCollectionParameterTest>
               {
                   public IReadOnlyCollectionParameterTestBuilder WithIds(IReadOnlyCollection<int> ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public IReadOnlyCollectionParameterTestBuilder WithEmptyIds()
                   {
                       return WithIds(new List<int>());
                   }
               }
               """;
    }
}