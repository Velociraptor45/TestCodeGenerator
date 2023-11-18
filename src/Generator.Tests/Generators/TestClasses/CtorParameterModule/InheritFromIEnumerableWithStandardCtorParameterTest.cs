using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class InheritFromIEnumerableWithStandardCtorParameterTest
{
    public InheritFromIEnumerableWithStandardCtorParameterTest(InheritFromIEnumerableWithStandardCtor ids)
    {
        Ids = ids;
    }

    public InheritFromIEnumerableWithStandardCtor Ids { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
               public class InheritFromIEnumerableWithStandardCtorParameterTestBuilder : DomainTestBuilderBase<InheritFromIEnumerableWithStandardCtorParameterTest>
               {
                   public InheritFromIEnumerableWithStandardCtorParameterTestBuilder WithIds(InheritFromIEnumerableWithStandardCtor ids)
                   {
                       FillConstructorWith(nameof(ids), ids);
                       return this;
                   }

                   public InheritFromIEnumerableWithStandardCtorParameterTestBuilder WithEmptyIds()
                   {
                       return WithIds(new InheritFromIEnumerableWithStandardCtor());
                   }
               }
               """;
    }
}