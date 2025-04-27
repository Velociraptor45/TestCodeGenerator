using RefleCS;
using RefleCS.Nodes;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Models;
using TestCodeGenerator.Generator.Modules.TestBuilder;

namespace TestCodeGenerator.Generator.Generators;

public class RandomTestBuilderGenerator : TestBuilderGeneratorBase
{
    private readonly List<ITestBuilderModule> _modules;

    public RandomTestBuilderGenerator(IFileHandler fileHandler, ICsFileHandler csFileHandler,
        BuilderConfiguration config, IEnumerable<ITestBuilderModule> modules) : base(fileHandler, csFileHandler, config)
    {
        _modules = modules.ToList();
    }


    protected override void UpdateFile(CsFile file, Type type, Usings usings, string builderClassName)
    {
        var cls = file.Nmsp.Classes.FirstOrDefault(c => c.Name == builderClassName);

        if (cls is null)
        {
            cls = Class.Public(builderClassName);
            file.Nmsp.AddClass(cls);
        }

        cls.RemoveAllBaseTypes();
        cls.AddBaseType(new BaseType($"{Config.GenericSuperclassTypeName}<{type.Name}>"));

        var methodsToRemove = cls.Methods
            .Where(m => !m.LeadingComments.Any(c => c.Value.ToLower().Contains("tcg keep")))
            .ToArray();

        foreach (var method in methodsToRemove)
        {
            cls.RemoveMethod(method);
        }

        foreach (var module in _modules)
        {
            module.Apply(type, builderClassName, cls, usings);
        }

        foreach (var @using in usings)
        {
            file.AddUsing(@using);
        }

        file.OrderUsingsAsc();
    }
}
