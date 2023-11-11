using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class InheritFromIEnumerableWithStandardCtorPropertyTest
{
    public InheritFromIEnumerableWithStandardCtor Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
               public class InheritFromIEnumerableWithStandardCtorPropertyTestBuilder : DomainTestBuilderBase<InheritFromIEnumerableWithStandardCtorPropertyTest>
               {
                   public InheritFromIEnumerableWithStandardCtorPropertyTestBuilder WithIds(InheritFromIEnumerableWithStandardCtor ids)
                   {
                       FillPropertyWith(p => p.Ids, ids);
                       return this;
                   }
               
                   public InheritFromIEnumerableWithStandardCtorPropertyTestBuilder WithEmptyIds()
                   {
                       return WithIds(new InheritFromIEnumerableWithStandardCtor());
                   }
               }
               """;
    }
}