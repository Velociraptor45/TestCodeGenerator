namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.NoRandomData;

public class BoolPropertyNoRandomTest
{
    public bool Id { get; set; }

    public static string GetExpectedBuilder()
    {
        return """
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.NoRandomData;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.NoRandomData;
               public class BoolPropertyNoRandomTestBuilder
               {
                   private BoolPropertyNoRandomTest _obj = new();
                   public BoolPropertyNoRandomTestBuilder WithId(bool id)
                   {
                       _obj.Id = id;
                       return this;
                   }

                   public BoolPropertyNoRandomTest Create()
                   {
                       return _obj;
                   }
               }
               """;
    }
}