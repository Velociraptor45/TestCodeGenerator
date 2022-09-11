using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

public class InheritFromIEnumerableWithoutStandardCtorParameterTest
{
    public InheritFromIEnumerableWithoutStandardCtorParameterTest(InheritFromIEnumerableWithoutStandardCtor ids)
    {
        Ids = ids;
    }

    public InheritFromIEnumerableWithoutStandardCtor Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CtorParameterModule;
public class InheritFromIEnumerableWithoutStandardCtorParameterTestBuilder : DomainTestBuilderBase<InheritFromIEnumerableWithoutStandardCtorParameterTest>
{
    public InheritFromIEnumerableWithoutStandardCtorParameterTestBuilder WithIds(InheritFromIEnumerableWithoutStandardCtor ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }
}";
    }
}