# TestCodeGenerator
The TestCodeGenerator generates C# builder classes for a given type (see [builder pattern](https://refactoring.guru/design-patterns/builder)). This is ideally combined with AutoFixture for random test data generation.<br/>
The generated builders will not build the object itself but rather provide the "With" methods and delegate the rest to a generic base class that you have to supply.

## Features
- Generates methods for ctor parameters and publicly mutable properties
- Detect multiple constructors and deduplicate parameters/properties
- Detect & handle non-generic parameter types that inherit from `IEnumerable<T>`, e.g. `List<T>`, `Dictionary<T1, T2>` or your own custom implementations
- Detect & handle nullable parameter types
- Support for generic types as generic type argument e.g. `IEnumerable<IEnumerable<int>>`
- Generate `WithEmpty` method for parameter types inheriting from `IEnumerable<T>` if they are classes
- Generate `Without` method for nullable parameter types
- Detect differences to an exising file and keep defined methods (see [here](#prevent-overwriting-methods))

## Limitations
- Nullability is currently only handled for parameter types that are explicitely labeled as nullable with a `?`
- No `WithEmpty` support for interfaces that inherit from `IEnumerable<T>` as parameter types e.g. `IList<T>`

## Cli Arguments
- `-c | --class` <br/>
The name of the class you want to create a builder for
- `-s | --settings` <br/>
The name of settings group in your appsettings file that you want to use

## appsetting configuration
You have to provide a set of settings in order to let the TestCodeGenerator know about classes, namespaces and methods. A dummy examply can be found under `TestCodeGenerator/Console/appsettings.json`

### Name
The name of the settings group that is used for the `-s` CLI command

### DllPath
The absolute path to the dll where your class is located

### OutputFolder
The folder where the builder class file will be created

### GenericSuperclassTypeName
The name of the superclass that the newly created builder class should inherit from

### GenericSuperclassNamespace
The namespace of the class specified in `GenericSuperclassTypeName`

### CtorInjectionMethodName
The method in the superclass that is used for ctor injections

### PropertyInjectionMethodName
The method in the superclass that is used for property injections

### OutputAssemblyRootNamespace
The root namespace of the assembly where the newly created builder class should be placed in

### BuilderNamePattern (optional)
The pattern with which the builder class name is generated. Use `{ClassName}` as the placeholder for the class's name for which you are generating the builder. Omit it for the default pattern `{ClassName}Builder`

### NullabilityEnabled (optional)
The indication whether the given class should be treated with enabled or disabled [nullability feature](https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references). Default: `true`

## Example
Let's say you have the following model,

```c#
using MyProject.Domain.Availabilities.Models;
using System;
using System.Collections.Generic;

namespace MyProject.Domain.Items.Models;

public class Item
{
    public Item(ItemId id, IEnumerable<IItemAvailability> availabilities, int? timesBought)
    {
        // parameter assignment
    }
    
    public double Price { get; set; }

    // here is the rest of your model's code
}
```

provide the following settings in your appsettings.json

```json
{
  "Settings": [
    {
      "Name": "Domain",
      "DllPath": "C:\\Repositories\\MyProject\\bin\\Debug\\net6.0\\MyProject.Domain.dll",
      "OutputFolder": "C:\\OutputFolder",
      "GenericSuperclassTypeName": "DomainTestBuilderBase",
      "GenericSuperclassNamespace": "MyProject.Domain.TestKit.Common",
      "CtorInjectionMethodName": "FillConstructorWith",
      "PropertyInjectionMethodName": "FillPropertyWith",
      "OutputAssemblyRootNamespace": "MyProject.Domain.TestKit",
      "BuilderNamePattern": "MyCool{ClassName}Builder"
    }
  ]
}
```

and call TestCodeGenerator with

```
Console.exe -c "Item" -s "Domain"
```

it will generate this builder:

```c#
using MyProject.Domain.Availabilities.Models;
using MyProject.Domain.Items.Models;
using MyProject.Domain.TestKit.Common;
using System;
using System.Collections.Generic;

namespace MyProject.Domain.TestKit.Items.Models;

public class MyCoolItemBuilder : DomainTestBuilderBase<Item>
{
    public MyCoolItemBuilder WithId(ItemId id)
    {
        FillConstructorWith(nameof(id), id);
        return this;
    }

    public MyCoolItemBuilder WithAvailabilities(IEnumerable<IItemAvailability> availabilities)
    {
        FillConstructorWith(nameof(availabilities), availabilities);
        return this;
    }

    public MyCoolItemBuilder WithEmptyAvailabilities()
    {
        return WithAvailabilities(Enumerable.Empty<IItemAvailability>());
    }

    public MyCoolItemBuilder WithTimesBought(int? timesBought)
    {
        FillConstructorWith(nameof(timesBought), timesBought);
        return this;
    }

    public MyCoolItemBuilder WithoutTimesBought()
    {
        return WithTimesBought(null);
    }
    
    public MyCoolItemBuilder WithPrice(double price)
    {
        FillPropertyWith(p => p.Price, price);
        return this;
    }
}
```

## Prevent overwriting methods
The TestCodeGenerator can detect whether there is already an existing file at the output location and analyzes it (only supported with file-scoped namespaces). If the file does not contain the class the TestCodeGenerator is going to generate, then a new class will be added. Otherwise all methods from the existing class will be removed and overwritten. However, if you want to keep certain methods you have to leave the comment `TCG keep` above it, e.g.
```cs
public class ItemBuilder
{
    // TCG keep
    public void Init() // this method will be kept
    {
    }
    
    public void AnotherInit() // this method will be removed
    {
    }
}
```

## Troubleshooting

### The dll could not be loaded
If the dll could not be loaded, you might be missing other dlls that your dll is referencing. .net sometimes needs these dlls in the same directory as your dll. Try adding this to your `.csproj` file and recompile. This will place all the dlls your dll is depending on in the same output directory
```
<PropertyGroup>
  <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
</PropertyGroup>
```
