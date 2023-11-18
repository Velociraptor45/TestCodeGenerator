namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class NullableEnumerableInEnumerableParameterTest
{
    public NullableEnumerableInEnumerableParameterTest(IEnumerable<IEnumerable<int>?> ids)
    {
        Ids = ids;
    }

    public IEnumerable<IEnumerable<int>?> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using System.Linq;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class NullableEnumerableInEnumerableParameterTestBuilder : DomainTestBuilderBase<NullableEnumerableInEnumerableParameterTest>
               {
                   public NullableEnumerableInEnumerableParameterTestBuilder WithIds(IEnumerable<IEnumerable<int>?> ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }
               
                   public NullableEnumerableInEnumerableParameterTestBuilder WithEmptyIds()
                   {
                       return WithIds(Enumerable.Empty<IEnumerable<int>?>());
                   }
               }
               """;
    }
}