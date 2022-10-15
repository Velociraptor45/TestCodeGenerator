using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class InheritFromIEnumerableWithoutStandardCtorPropertyTest
{
    public InheritFromIEnumerableWithoutStandardCtor Ids { get; set; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
public class InheritFromIEnumerableWithoutStandardCtorPropertyTestBuilder : DomainTestBuilderBase<InheritFromIEnumerableWithoutStandardCtorPropertyTest>
{
    public InheritFromIEnumerableWithoutStandardCtorPropertyTestBuilder WithIds(InheritFromIEnumerableWithoutStandardCtor ids)
    {
        FillPropertyWith(p => p.Ids, ids);
        return this;
    }
}";
    }
}