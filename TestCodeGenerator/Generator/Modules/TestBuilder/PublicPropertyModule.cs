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

    public override void Apply(Type type, string builderClassName, Class cls, Namespaces namespaces)
    {
        var properties = new Dictionary<(string, string), PropertyInfo>();

        var publicSetProperties = type
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.GetSetMethod() != null);

        foreach (var property in publicSetProperties)
        {
            var key = (property.Name, property.PropertyType.FullName!);
            properties.Add(key, property);
        }

        foreach (var info in properties.Values)
        {
            var nullabilityInfo = new NullabilityInfoContext().Create(info);
            AddMethods(nullabilityInfo, info.PropertyType, info.Name, builderClassName, cls, namespaces);
        }
    }

    protected override Statement GetWithStatement(string originalName)
    {
        return new(
            $"{Config.PropertyInjectionMethodName}(p => p.{originalName}, {originalName.LowercaseFirstLetter()});");
    }

    protected override string GetParameterName(string originalName)
    {
        return originalName.LowercaseFirstLetter();
    }
}