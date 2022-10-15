namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CustomizedBuilderName;

public class WithoutClassNamePatternTest
{
    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CustomizedBuilderName;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CustomizedBuilderName;
public class WithoutClassNamePatternTestBuilder : DomainTestBuilderBase<WithoutClassNamePatternTest>
{
}";
    }

    public static string? GetBuilderNamePattern()
    {
        return null;
    }

    public static string GetFileName()
    {
        return "WithoutClassNamePatternTestBuilder";
    }
}