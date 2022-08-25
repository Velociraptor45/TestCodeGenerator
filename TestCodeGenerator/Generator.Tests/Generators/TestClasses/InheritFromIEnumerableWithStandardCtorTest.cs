using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses;

public class InheritFromIEnumerableWithStandardCtorTest
{
    public InheritFromIEnumerableWithStandardCtorTest(InheritFromIEnumerableWithStandardCtor ids)
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
public class InheritFromIEnumerableWithStandardCtorTestBuilder : DomainTestBuilderBase<InheritFromIEnumerableWithStandardCtorTest>
{
    public InheritFromIEnumerableWithStandardCtorTestBuilder WithIds(InheritFromIEnumerableWithStandardCtor ids)
    {
        FillConstructorWith(nameof(ids), ids);
        return this;
    }

    public InheritFromIEnumerableWithStandardCtorTestBuilder WithEmptyIds()
    {
        return WithIds(new InheritFromIEnumerableWithStandardCtor());
    }
}";
    }
}