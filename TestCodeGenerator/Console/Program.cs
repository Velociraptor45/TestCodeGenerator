using Generator;
using Generator.Configurations;
using Generator.Files;

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

/*
 * todo:
 * - output namespace
 * - detect diff to existing builder file
 */