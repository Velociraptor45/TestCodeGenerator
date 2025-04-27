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


    protected override bool UpdateFile(CsFile file, Type type, string builderClassName)
    {
        var usings = new Usings { Config.GenericSuperclassNamespace, type.Namespace! };

        var cls = file.Nmsp.Classes.FirstOrDefault(c => c.Name == builderClassName);

        if (cls is null)
        {
            cls = Class.Public(builderClassName);
            file.Nmsp.AddClass(cls);
        }

        cls.RemoveAllBaseTypes();
        cls.AddBaseType(new BaseType($"{Config.GenericSuperclassTypeName}<{type.Name}>"));

        RemoveAllGeneratedMethods(cls);

        foreach (var module in _modules)
        {
            module.Apply(type, builderClassName, cls, usings);
        }

        foreach (var @using in usings)
        {
            file.AddUsing(@using);
        }

        file.OrderUsingsAsc();
        return true;
    }
}
