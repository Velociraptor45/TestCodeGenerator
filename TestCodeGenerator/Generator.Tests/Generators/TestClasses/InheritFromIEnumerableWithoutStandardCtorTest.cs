using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class InheritFromIEnumerableWithoutStandardCtorTest
{
    public InheritFromIEnumerableWithoutStandardCtorTest(InheritFromIEnumerableWithoutStandardCtor ids)
    {
        Ids = ids;
    }

    public InheritFromIEnumerableWithoutStandardCtor Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class InheritFromIEnumerableWithoutStandardCtorTestBuilder : DomainTestBuilderBase<InheritFromIEnumerableWithoutStandardCtorTest>
{
    public InheritFromIEnumerableWithoutStandardCtorTestBuilder WithIds(InheritFromIEnumerableWithoutStandardCtor ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }
}
";
    }
}