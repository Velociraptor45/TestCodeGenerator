namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class IDictionaryParameterTest
{
    public IDictionaryParameterTest(IDictionary<int, string> ids)
    {
        Ids = ids;
    }

    public IDictionary<int, string> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class IDictionaryParameterTestBuilder : DomainTestBuilderBase<IDictionaryParameterTest>
               {
                   public IDictionaryParameterTestBuilder WithIds(IDictionary<int, string> ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public IDictionaryParameterTestBuilder WithEmptyIds()
                   {
                       return WithIds(new Dictionary<int, string>());
                   }
               }
               """;
    }
}