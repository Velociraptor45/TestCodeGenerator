﻿using RefleCS.Nodes;
using System.Reflection;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Models;

namespace TestCodeGenerator.Generator.Modules.TestBuilder;

public class CtorParameterModule : TestBuilderModuleBase
{
    public CtorParameterModule(BuilderConfiguration config) : base(config)
    {
    }

    public override void Apply(Type type, string builderClassName, Class cls, Usings usings)
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
            AddMethods(nullabilityInfo, info.ParameterType, info.Name!, builderClassName, cls, usings);
        }
    }

    protected override Statement GetWithStatement(string originalName, string withMethodParameterName)
    {
        var nameParameter = originalName == withMethodParameterName
            ? $"nameof({originalName})"
            : $"\"{originalName}\"";

        return new($"{Config.CtorInjectionMethodName}({nameParameter}, {withMethodParameterName});");
    }
}