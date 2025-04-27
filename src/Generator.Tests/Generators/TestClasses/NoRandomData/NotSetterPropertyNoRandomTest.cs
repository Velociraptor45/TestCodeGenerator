namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.NoRandomData;

public class NotSetterPropertyNoRandomTest
{
    public bool Id { get; } = true;

    public static string GetExpectedBuilder()
    {
        return """
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.NoRandomData;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.NoRandomData;
               public class NotSetterPropertyNoRandomTestBuilder
               {
                   private NotSetterPropertyNoRandomTest _obj = new();
                   public NotSetterPropertyNoRandomTest Create()
                   {
                       return _obj;
                   }
               }
               """;
    }
}