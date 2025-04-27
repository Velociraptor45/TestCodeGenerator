namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.NoRandomData;

public class AdditionalCtorNoRandomTest
{
    public AdditionalCtorNoRandomTest()
    {
    }

    public AdditionalCtorNoRandomTest(bool id)
    {
        Id = id;
    }

    public bool Id { get; set; }

    public static string GetExpectedBuilder()
    {
        return """
               using System;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.NoRandomData;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.NoRandomData;
               public class AdditionalCtorNoRandomTestBuilder
               {
                   private AdditionalCtorNoRandomTest _obj = new();
                   public AdditionalCtorNoRandomTestBuilder WithId(bool id)
                   {
                       _obj.Id = id;
                       return this;
                   }
               
                   public AdditionalCtorNoRandomTest Create()
                   {
                       return _obj;
                   }
               }
               """;
    }
}
