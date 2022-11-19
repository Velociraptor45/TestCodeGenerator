namespace TestCodeGenerator.Generator.Configurations;

public class BuilderConfiguration
{
    public string Name { get; init; } = string.Empty;
    public string DllPath { get; init; } = string.Empty;
    public string OutputFolder { get; init; } = string.Empty;
    public string GenericSuperclassTypeName { get; init; } = string.Empty;
    public string GenericSuperclassNamespace { get; init; } = string.Empty;
    public string CtorInjectionMethodName { get; init; } = string.Empty;
    public string PropertyInjectionMethodName { get; init; } = string.Empty;
    public string OutputAssemblyRootNamespace { get; init; } = string.Empty;
    public string? BuilderNamePattern { get; init; } = null;
    public bool NullabilityEnabled { get; init; } = true;
}