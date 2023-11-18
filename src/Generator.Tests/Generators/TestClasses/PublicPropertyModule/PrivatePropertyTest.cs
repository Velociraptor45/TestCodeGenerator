namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

public class PrivatePropertyTest
{
    private bool Id { get; set; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.PublicPropertyModule;
               public class PrivatePropertyTestBuilder : DomainTestBuilderBase<PrivatePropertyTest>
               {
               }
               """;
    }
}