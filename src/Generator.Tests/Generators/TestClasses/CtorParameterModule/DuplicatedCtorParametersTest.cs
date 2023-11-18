namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class DuplicatedCtorParametersTest
{
    public DuplicatedCtorParametersTest(IEnumerable<char> ids)
    {
        Ids = ids;
    }

    public DuplicatedCtorParametersTest(int id, IEnumerable<char> ids)
    {
        Ids = ids;
    }

    public IEnumerable<char> Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using System.Linq;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class DuplicatedCtorParametersTestBuilder : DomainTestBuilderBase<DuplicatedCtorParametersTest>
               {
                   public DuplicatedCtorParametersTestBuilder WithIds(IEnumerable<char> ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public DuplicatedCtorParametersTestBuilder WithEmptyIds()
                   {
                       return WithIds(Enumerable.Empty<char>());
                   }

                   public DuplicatedCtorParametersTestBuilder WithId(int id)
                   {
                       FillConstructorWith(nameof(id), id);
                       return this;
                   }
               }
               """;
    }
}