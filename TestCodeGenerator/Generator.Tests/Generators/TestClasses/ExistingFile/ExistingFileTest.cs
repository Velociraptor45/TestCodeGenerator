namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.ExistingFile;

public class ExistingFileTest
{
    public bool Id { get; set; }

    public static string GetExistingBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.ExistingFile;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.ExistingFile;
public class ExistingFileTestBuilder : DomainTestBuilderBase<ExistingFileTest>
{
    public ExistingFileTestBuilder WithId2(bool id)
    {
        FillPropertyWith(p => p.Id, id);
        return this;
    }
}";
    }

    public static string GetExpectedBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.ExistingFile;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.ExistingFile;
public class ExistingFileTestBuilder : DomainTestBuilderBase<ExistingFileTest>
{
    public ExistingFileTestBuilder WithId(bool id)
    {
        FillPropertyWith(p => p.Id, id);
        return this;
    }
}";
    }
}