# TestCodeGenerator
The TestCodeGenerator will automatically generate C# builder classes for a given type (see [builder pattern](https://refactoring.guru/design-patterns/builder)). This is ideally combined with AutoFixture for random test data generation.<br/>
The generated builders will not build the object itself but rather provide the "With" methods and delegate the rest to a generic base class that you have to supply.

## Features
- Detect multiple constructors and deduplicate parameters
- Detect & handle non-generic parameter types that inherit from `IEnumerable<T>`, e.g. `List<T>`, `Dictionary<T1, T2>` or your own custom implementations
- Detect & handle nullable parameter types
- Support for generic types as generic type argument e.g. `IEnumerable<IEnumerable<int>>`
- Generate `WithEmpty` method for parameter types inheriting from `IEnumerable<T>` if they are classes
- Generate `Without` method for nullable parameter types

## Limitations
- Nullability is currently only handled for parameter types that are explicitely labeled as nullable with a `?`
- No `WithEmpty` support for interfaces that inherit from `IEnumerable<T>` as parameter types e.g. `IList<T>`

## Cli Arguments
- `-c` | `--class` <br/>
The name of the class you want to create a builder for
- `-s | --settings` <br/>
The name of settings object that you want to use in your appsettings file

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
      "OutputAssemblyRootNamespace": "MyProject.Domain.TestKit"
    }
  ]
}
```

and call TestCodeGenerator with

```
TestCodeGenerator.exe -c "Item" -s "Domain"
```

it will generate this builder:

```c#
using MyProject.Domain.Availabilities.Models;
using MyProject.Domain.Items.Models;
using MyProject.Domain.TestKit.Common;
using System;
using System.Collections.Generic;

namespace MyProject.Domain.TestKit.Items.Models;

public class ItemBuilder : DomainTestBuilderBase<Item>
{
    public ItemBuilder WithId(ItemId id)
    {
        FillConstructorWith(nameof(id), id);
        return this;
    }

    public ItemBuilder WithAvailabilities(IEnumerable<IItemAvailability> availabilities)
    {
        FillConstructorWith(nameof(availabilities), availabilities);
        return this;
    }

    public ItemBuilder WithEmptyAvailabilities()
    {
        return WithAvailabilities(Enumerable.Empty<IItemAvailability>());
    }

    public ItemBuilder WithTimesBought(int? timesBought)
    {
        FillConstructorWith(nameof(timesBought), timesBought);
        return this;
    }

    public ItemBuilder WithoutTimesBought()
    {
        return WithTimesBought(null);
    }
}
```