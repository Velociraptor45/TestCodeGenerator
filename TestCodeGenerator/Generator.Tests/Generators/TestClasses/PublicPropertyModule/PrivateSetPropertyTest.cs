namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class PrivateSetPropertyTest
{
    public bool Id { get; private set; }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
public class PrivateSetPropertyTestBuilder : DomainTestBuilderBase<PrivateSetPropertyTest>
{
}";
    }
}