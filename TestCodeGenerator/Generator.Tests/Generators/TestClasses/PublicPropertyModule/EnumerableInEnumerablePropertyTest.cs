namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class EnumerableInEnumerablePropertyTest
{
    public IEnumerable<IEnumerable<int>> Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using System.Linq;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
               public class EnumerableInEnumerablePropertyTestBuilder : DomainTestBuilderBase<EnumerableInEnumerablePropertyTest>
               {
                   public EnumerableInEnumerablePropertyTestBuilder WithIds(IEnumerable<IEnumerable<int>> ids)
                   {
                       FillPropertyWith(p => p.Ids, ids);
                       return this;
                   }
               
                   public EnumerableInEnumerablePropertyTestBuilder WithEmptyIds()
                   {
                       return WithIds(Enumerable.Empty<IEnumerable<int>>());
                   }
               }
               """;
    }
}