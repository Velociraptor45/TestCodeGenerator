namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CustomizedBuilderName;

public class WithStaticClassNamePatternTest
{
    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CustomizedBuilderName;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CustomizedBuilderName;
public class EntityBuilder : DomainTestBuilderBase<WithStaticClassNamePatternTest>
{
}";
    }

    public static string GetBuilderNamePattern()
    {
        return "EntityBuilder";
    }

    public static string GetFileName()
    {
        return "EntityBuilder";
    }
}