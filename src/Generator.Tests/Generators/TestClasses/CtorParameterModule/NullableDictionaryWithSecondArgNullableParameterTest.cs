﻿namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class NullableDictionaryWithSecondArgNullableParameterTest
{
    public NullableDictionaryWithSecondArgNullableParameterTest(Dictionary<int, string?>? ids)
    {
        Ids = ids;
    }

    public Dictionary<int, string?>? Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class NullableDictionaryWithSecondArgNullableParameterTestBuilder : DomainTestBuilderBase<NullableDictionaryWithSecondArgNullableParameterTest>
               {
                   public NullableDictionaryWithSecondArgNullableParameterTestBuilder WithIds(Dictionary<int, string?>? ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public NullableDictionaryWithSecondArgNullableParameterTestBuilder WithEmptyIds()
                   {
                       return WithIds(new Dictionary<int, string?>());
                   }

                   public NullableDictionaryWithSecondArgNullableParameterTestBuilder WithoutIds()
                   {
                       return WithIds(null);
                   }
               }
               """;
    }
}