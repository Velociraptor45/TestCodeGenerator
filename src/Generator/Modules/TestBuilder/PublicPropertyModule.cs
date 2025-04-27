using RefleCS.Nodes;
using System.Reflection;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Extensions;
using TestCodeGenerator.Generator.Models;

namespace TestCodeGenerator.Generator.Modules.TestBuilder;

public class PublicPropertyModule : TestBuilderModuleBase
{
    public PublicPropertyModule(BuilderConfiguration config) : base(config)
    {
    }

    public override void Apply(Type type, string builderClassName, Class cls, Usings usings)
    {
        var properties = new Dictionary<(string, string), PropertyInfo>();

        var publicSetProperties = type
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.GetSetMethod() != null);

        // When not using random data, the object is created at first before the properties are set
        // Thus, we can't write to properties that don't have a public set
        if (!Config.UseRandomData)
            publicSetProperties = publicSetProperties.Where(p => p.SetMethod is not null
                                                                 && p.SetMethod.IsPublic
                                                                 && !p.SetMethod.IsInitOnly());

        foreach (var property in publicSetProperties)
        {
            var key = (property.Name, property.PropertyType.FullName!);
            properties.Add(key, property);
        }

        foreach (var info in properties.Values)
        {
            var nullabilityInfo = new NullabilityInfoContext().Create(info);
            AddMethods(nullabilityInfo, info.PropertyType, info.Name, builderClassName, cls, usings);
        }
    }

    protected override Statement GetWithStatement(string originalName, string withMethodParameterName)
    {
        if (Config.UseRandomData)
        {
            return new(
                $"{Config.PropertyInjectionMethodName}(p => p.{originalName}, {originalName.LowercaseFirstLetter()});");
        }

        return new($"_obj.{originalName} = {originalName.LowercaseFirstLetter()};");
    }
}