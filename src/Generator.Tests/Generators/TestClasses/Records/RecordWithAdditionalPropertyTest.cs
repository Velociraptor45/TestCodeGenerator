namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.Records;
public record RecordWithAdditionalPropertyTest(int? Id)
{
    public string Name { get; set; }

    public static string GetExpectedBuilder()
    {
        return
            """
            using Superclass.Namespace;
            using System;
            using TestCodeGenerator.Generator.Tests.Generators.TestClasses.Records;

            namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.Records;
            public class RecordWithAdditionalPropertyTestBuilder : DomainTestBuilderBase<RecordWithAdditionalPropertyTest>
            {
                public RecordWithAdditionalPropertyTestBuilder WithId(int? id)
                {
                    FillConstructorWith("Id", id);
                    return this;
                }

                public RecordWithAdditionalPropertyTestBuilder WithoutId()
                {
                    return WithId(null);
                }

                public RecordWithAdditionalPropertyTestBuilder WithName(string name)
                {
                    FillPropertyWith(p => p.Name, name);
                    return this;
                }
            }
            """;
    }
}