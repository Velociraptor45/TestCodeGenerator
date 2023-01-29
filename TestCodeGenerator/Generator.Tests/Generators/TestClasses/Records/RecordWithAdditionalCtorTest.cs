namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.Records;
public record RecordWithAdditionalCtorTest(int? Id)
{
    public RecordWithAdditionalCtorTest(int? id, string name) : this(id)
    {
        Name = name;
    }

    public string Name { get; }

    public static string GetExpectedBuilder()
    {
        return
            """
            using Superclass.Namespace;
            using System;
            using TestCodeGenerator.Generator.Tests.Generators.TestClasses.Records;

            namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.Records;
            public class RecordWithAdditionalCtorTestBuilder : DomainTestBuilderBase<RecordWithAdditionalCtorTest>
            {
                public RecordWithAdditionalCtorTestBuilder WithId(int? id)
                {
                    FillConstructorWith("Id", id);
                    return this;
                }

                public RecordWithAdditionalCtorTestBuilder WithoutId()
                {
                    return WithId(null);
                }

                public RecordWithAdditionalCtorTestBuilder WithName(string name)
                {
                    FillConstructorWith(nameof(name), name);
                    return this;
                }
            }
            """;
    }
}