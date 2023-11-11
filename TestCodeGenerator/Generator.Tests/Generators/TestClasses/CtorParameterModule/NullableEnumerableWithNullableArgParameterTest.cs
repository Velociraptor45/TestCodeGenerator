namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class NullableEnumerableWithNullableArgParameterTest
{
    public NullableEnumerableWithNullableArgParameterTest(IEnumerable<int?>? ids)
    {
        Ids = ids;
    }

    public IEnumerable<int?>? Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using System.Linq;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class NullableEnumerableWithNullableArgParameterTestBuilder : DomainTestBuilderBase<NullableEnumerableWithNullableArgParameterTest>
               {
                   public NullableEnumerableWithNullableArgParameterTestBuilder WithIds(IEnumerable<int?>? ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public NullableEnumerableWithNullableArgParameterTestBuilder WithEmptyIds()
                   {
                       return WithIds(Enumerable.Empty<int?>());
                   }

                   public NullableEnumerableWithNullableArgParameterTestBuilder WithoutIds()
                   {
                       return WithIds(null);
                   }
               }
               """;
    }
}