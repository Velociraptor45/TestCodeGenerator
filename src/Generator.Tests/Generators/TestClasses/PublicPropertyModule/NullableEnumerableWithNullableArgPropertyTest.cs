namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class NullableEnumerableWithNullableArgPropertyTest
{
    public IEnumerable<int?>? Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using System.Linq;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
               public class NullableEnumerableWithNullableArgPropertyTestBuilder : DomainTestBuilderBase<NullableEnumerableWithNullableArgPropertyTest>
               {
                   public NullableEnumerableWithNullableArgPropertyTestBuilder WithIds(IEnumerable<int?>? ids)
                   {
                       FillPropertyWith(p => p.Ids, ids);
                       return this;
                   }
               
                   public NullableEnumerableWithNullableArgPropertyTestBuilder WithEmptyIds()
                   {
                       return WithIds(Enumerable.Empty<int?>());
                   }
               
                   public NullableEnumerableWithNullableArgPropertyTestBuilder WithoutIds()
                   {
                       return WithIds(null);
                   }
               }
               """;
    }
}