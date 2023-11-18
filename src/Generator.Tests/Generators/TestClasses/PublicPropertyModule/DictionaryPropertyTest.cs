namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class DictionaryPropertyTest
{
    public Dictionary<int, string> Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
               public class DictionaryPropertyTestBuilder : DomainTestBuilderBase<DictionaryPropertyTest>
               {
                   public DictionaryPropertyTestBuilder WithIds(Dictionary<int, string> ids)
                   {
                       FillPropertyWith(p => p.Ids, ids);
                       return this;
                   }
               
                   public DictionaryPropertyTestBuilder WithEmptyIds()
                   {
                       return WithIds(new Dictionary<int, string>());
                   }
               }
               """;
    }
}