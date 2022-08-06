using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class InheritFromIEnumerableWithoutStandardCtorTest
{
    public InheritFromIEnumerableWithoutStandardCtorTest(InheritFromIEnumerableWithStandardCtor ids)
    {
        Ids = ids;
    }

    public InheritFromIEnumerableWithStandardCtor Ids { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses;

public class InheritFromIEnumerableWithoutStandardCtorTestBuilder : DomainTestBuilderBase<InheritFromIEnumerableWithoutStandardCtorTest>
{
    public InheritFromIEnumerableWithoutStandardCtorTestBuilder WithIds(InheritFromIEnumerableWithStandardCtor ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }
}
";
    }
}