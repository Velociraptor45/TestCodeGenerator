namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.NoRandomData;

public class InitPropertyNoRandomTest
{
    public bool Id { get; init; }

    public static string GetExpectedBuilder()
    {
        return """
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.NoRandomData;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.NoRandomData;
               public class InitPropertyNoRandomTestBuilder
               {
                   private InitPropertyNoRandomTest _obj = new();
                   public InitPropertyNoRandomTest Create()
                   {
                       return _obj;
                   }
               }
               """;
    }
}