namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.CustomizedBuilderName;

public class WithClassNamePatternTest
{
    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CustomizedBuilderName;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.CustomizedBuilderName;
               public class WithClassNamePatternTestEntityBuilder : DomainTestBuilderBase<WithClassNamePatternTest>
               {
               }
               """;
    }

    public static string GetBuilderNamePattern()
    {
        return "{ClassName}EntityBuilder";
    }

    public static string GetFileName()
    {
        return "WithClassNamePatternTestEntityBuilder";
    }
}