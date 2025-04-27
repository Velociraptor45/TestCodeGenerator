using RefleCS;
using RefleCS.Enums;
using RefleCS.Nodes;
using TestCodeGenerator.Generator.Common;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Models;
using TestCodeGenerator.Generator.Modules.TestBuilder;

namespace TestCodeGenerator.Generator.Generators;

public class NoRandomTestBuilderGenerator : TestBuilderGeneratorBase
{
    private readonly List<ITestBuilderModule> _modules;

    public NoRandomTestBuilderGenerator(IFileHandler fileHandler, ICsFileHandler csFileHandler,
        BuilderConfiguration config, IEnumerable<ITestBuilderModule> modules) : base(fileHandler, csFileHandler, config)
    {
        _modules = modules.ToList();
    }

    protected override bool UpdateFile(CsFile file, Type type, string builderClassName)
    {
        if (type.GetConstructors().All(c => c.GetParameters().Length != 0))
        {
            Output.WriteError($"Type '{type.Name}' does not have a parameterless constructor. Cannot generate builder without random data.");
            return false;
        }

        var usings = new Usings { type.Namespace! };
        var cls = file.Nmsp.Classes.FirstOrDefault(c => c.Name == builderClassName);

        if (cls is null)
        {
            cls = Class.Public(builderClassName);
            file.Nmsp.AddClass(cls);
        }

        RemoveAllGeneratedMethods(cls);
        RemoveAllGeneratedFields(cls);

        cls.RemoveAllBaseTypes();
        cls.AddField(new Field([FieldModifier.Private], type.Name, "_obj", new("new()")));


        foreach (var module in _modules)
        {
            module.Apply(type, builderClassName, cls, usings);
        }

        var createMethod = new Method(type.Name, "Create")
            .AddStatement(new Statement("return _obj;"))
            .AddModifier(MethodModifier.Public);
        cls.AddMethod(createMethod);

        foreach (var @using in usings)
        {
            file.AddUsing(@using);
        }

        file.OrderUsingsAsc();
        return true;
    }


    protected void RemoveAllGeneratedFields(Class cls)
    {
        foreach (var field in cls.Fields.ToList())
        {
            cls.RemoveField(field);
        }
    }
}
