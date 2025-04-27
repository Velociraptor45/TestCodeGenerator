namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.NoRandomData;

public class PrivatelySettablePropertyNoRandomTest
{
    public bool Id { get; private set; }

    public static string GetExpectedBuilder()
    {
        return """
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.NoRandomData;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.NoRandomData;
               public class PrivatelySettablePropertyNoRandomTestBuilder
               {
                   private PrivatelySettablePropertyNoRandomTest _obj = new();
                   public PrivatelySettablePropertyNoRandomTest Create()
                   {
                       return _obj;
                   }
               }
               """;
    }
}