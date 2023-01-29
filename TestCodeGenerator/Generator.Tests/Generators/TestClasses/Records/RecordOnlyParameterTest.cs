namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.Records;
public record RecordOnlyParameterTest(int? Id)
{
    public static string GetExpectedBuilder()
    {
        return
            """
            using Superclass.Namespace;
            using System;
            using TestCodeGenerator.Generator.Tests.Generators.TestClasses.Records;

            namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.Records;
            public class RecordOnlyParameterTestBuilder : DomainTestBuilderBase<RecordOnlyParameterTest>
            {
                public RecordOnlyParameterTestBuilder WithId(int? id)
                {
                    FillConstructorWith("Id", id);
                    return this;
                }

                public RecordOnlyParameterTestBuilder WithoutId()
                {
                    return WithId(null);
                }
            }
            """;
    }
}