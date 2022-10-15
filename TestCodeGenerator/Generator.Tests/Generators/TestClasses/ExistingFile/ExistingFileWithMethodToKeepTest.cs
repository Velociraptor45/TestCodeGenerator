namespace TestCodeGenerator.Generator.Tests.Generators.TestClasses.ExistingFile;

public class ExistingFileWithMethodToKeepTest
{
    public bool Id { get; set; }

    public static string GetExistingBuilder()
    {
        return @"using Superclass.Namespace;
using System;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.ExistingFile;

namespace TestCodeGenerator.Generator.Tests.Tests.Generators.TestClasses.ExistingFile;
public class ExistingFileWithMethodToKeepTestBuilder : DomainTestBuilderBase<ExistingFileWithMethodToKeepTest>
{
    // tcg keep
    public ExistingFileWithMethodToKeepTestBuilder WithName(string name)
    {
        FillPropertyWith(p => p.Name, name);
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
public class ExistingFileWithMethodToKeepTestBuilder : DomainTestBuilderBase<ExistingFileWithMethodToKeepTest>
{
    // tcg keep
    public ExistingFileWithMethodToKeepTestBuilder WithName(string name)
    {
        FillPropertyWith(p => p.Name, name);
        return this;
    }

    public ExistingFileWithMethodToKeepTestBuilder WithId(bool id)
    {
        FillPropertyWith(p => p.Id, id);
        return this;
    }
}";
    }
}