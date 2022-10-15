namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class GetOnlyPropertyTest
{
    public bool Id { get; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
public class GetOnlyPropertyTestBuilder : DomainTestBuilderBase<GetOnlyPropertyTest>
{
}";
    }
}