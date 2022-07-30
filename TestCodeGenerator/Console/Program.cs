using Microsoft.Extensions.Configuration;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Generators;
using TestCodeGenerator.Generator.Services;

var appsettingsName = "";

var cfg = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{appsettingsName}.json",
                optional: false, reloadOnChange: false)
            .Build();

var config = new BuilderConfiguration
{
    DllPath = @"H:\Programming\Repositories\ProjectHermes\ShoppingList\Api\ShoppingList.Api.Domain\bin\Debug\net6.0\ShoppingList.Api.Domain.dll",
    OutputFolder = @"E:\Personal\Downloads",
    GenericSuperclassTypeName = "DomainTestBuilderBase",
    GenericSuperclassNamespace = "ProjectHermes.ShoppingList.Api.Domain.TestKit.Common",
    CtorInjectionMethodName = "FillConstructorWith",
    OutputAssemblyRootNamespace = "ProjectHermes.ShoppingList.Api.Domain.TestKit"
};

new TestBuilderGenerator(new FileHandler(), new TypeResolver(), config)
    .Generate("Item");