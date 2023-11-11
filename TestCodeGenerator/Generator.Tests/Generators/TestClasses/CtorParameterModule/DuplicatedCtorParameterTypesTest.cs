namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "<Pending>")]
public class DuplicatedCtorParameterTypesTest
{
    public DuplicatedCtorParameterTypesTest(int ids)
    {
    }

    public DuplicatedCtorParameterTypesTest(int id, IEnumerable<char> ids)
    {
    }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using System;
               using System.Collections.Generic;
               using System.Linq;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class DuplicatedCtorParameterTypesTestBuilder : DomainTestBuilderBase<DuplicatedCtorParameterTypesTest>
               {
                   public DuplicatedCtorParameterTypesTestBuilder WithIds(int ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public DuplicatedCtorParameterTypesTestBuilder WithId(int id)
                   {
                       FillConstructorWith(nameof(id), id);
                       return this;
                   }

                   public DuplicatedCtorParameterTypesTestBuilder WithIds(IEnumerable<char> ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public DuplicatedCtorParameterTypesTestBuilder WithEmptyIds()
                   {
                       return WithIds(Enumerable.Empty<char>());
                   }
               }
               """;
    }
}