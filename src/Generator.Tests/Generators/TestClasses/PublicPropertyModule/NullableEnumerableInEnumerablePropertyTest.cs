namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class NullableEnumerableInEnumerablePropertyTest
{
    public IEnumerable<IEnumerable<int>?> Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using System.Linq;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
               public class NullableEnumerableInEnumerablePropertyTestBuilder : DomainTestBuilderBase<NullableEnumerableInEnumerablePropertyTest>
               {
                   public NullableEnumerableInEnumerablePropertyTestBuilder WithIds(IEnumerable<IEnumerable<int>?> ids)
                   {
                       FillPropertyWith(p => p.Ids, ids);
                       return this;
                   }

                   public NullableEnumerableInEnumerablePropertyTestBuilder WithEmptyIds()
                   {
                       return WithIds(Enumerable.Empty<IEnumerable<int>?>());
                   }
               }
               """;
    }
}