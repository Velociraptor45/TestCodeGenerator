namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class NullableDictionaryWithSecondArgNullablePropertyTest
{
    public Dictionary<int, string?>? Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
               public class NullableDictionaryWithSecondArgNullablePropertyTestBuilder : DomainTestBuilderBase<NullableDictionaryWithSecondArgNullablePropertyTest>
               {
                   public NullableDictionaryWithSecondArgNullablePropertyTestBuilder WithIds(Dictionary<int, string?>? ids)
                   {
                       FillPropertyWith(p => p.Ids, ids);
                       return this;
                   }
               
                   public NullableDictionaryWithSecondArgNullablePropertyTestBuilder WithEmptyIds()
                   {
                       return WithIds(new Dictionary<int, string?>());
                   }
               
                   public NullableDictionaryWithSecondArgNullablePropertyTestBuilder WithoutIds()
                   {
                       return WithIds(null);
                   }
               }
               """;
    }
}