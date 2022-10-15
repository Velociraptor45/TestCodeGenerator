using RefleCS.Nodes;
using System.Reflection;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Models;

namespace TestCodeGenerator.Generator.Modules.TestBuilder;

public class CtorParameterModule : TestBuilderModuleBase
{
    private readonly BuilderConfiguration _config;

    public CtorParameterModule(BuilderConfiguration config)
    {
        _config = config;
    }

    public override void Apply(Type type, string builderClassName, Class cls, Namespaces namespaces)
    {
        var parameters = new Dictionary<(string, string), ParameterInfo>();

        var allCtorParameters = type.GetConstructors().SelectMany(ctor => ctor.GetParameters());
        foreach (var parameter in allCtorParameters)
        {
            var key = (parameter.Name!, parameter.ParameterType.FullName!);
            if (parameters.ContainsKey(key))
                continue;

            parameters.Add(key, parameter);
        }

        foreach (var info in parameters.Values)
        {
            var nullabilityInfo = new NullabilityInfoContext().Create(info);
            AddMethods(nullabilityInfo, info.ParameterType, info.Name!, builderClassName, cls, namespaces);
        }
    }

    protected override Statement GetWithStatement(string originalName)
    {
        return new($"{_config.CtorInjectionMethodName}(nameof({originalName}), {originalName});");
    }

    protected override string GetParameterName(string originalName)
    {
        return originalName;
    }
}