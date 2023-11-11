using TestCodeGenerator.Generator.Tests.Generators.Subclasses;

namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.WithoutNullability;

public class ClassParameterTest
{
    public ClassParameterTest(ClassDummy id)
    {
        Id = id;
    }

    public ClassDummy Id { get; }

    public static string GetExpectedBuilder()
    {
        return """
               using Superclass.Namespace;
               using TestCodeGenerator.Generator.Tests.Generators.Subclasses;
               using TestCodeGenerator.Generator.Tests.Generators.TestClasses.WithoutNullability;

               namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.WithoutNullability;
               public class ClassParameterTestBuilder : DomainTestBuilderBase<ClassParameterTest>
               {
                   public ClassParameterTestBuilder WithId(ClassDummy id)
                   {
                       FillConstructorWith(nameof(id), id);
                       return this;
                   }
               
                   public ClassParameterTestBuilder WithoutId()
                   {
                       return WithId(null);
                   }
               }
               """;
    }
}