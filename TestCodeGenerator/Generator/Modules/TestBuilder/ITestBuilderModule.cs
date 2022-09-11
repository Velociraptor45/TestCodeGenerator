﻿using RefleCS.Nodes;
using TestCodeGenerator.Generator.Generators;
using TestCodeGenerator.Generator.Models;

namespace TestCodeGenerator.Generator.Modules.TestBuilder;

public interface ITestBuilderModule
{
    void Apply(Type type, string builderClassName, Class cls, Namespaces namespaces);
}