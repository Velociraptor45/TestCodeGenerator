using FluentAssertions;
using TestCodeGenerator.Generator.Models;

namespace TestCodeGenerator.Generator.Tests.Models;
public class NamespaceInfoTests
{
    [Theory]
    [InlineData("TestCodeGenerator.Generator", "TestCodeGenerator.Generator.Models", "Models")]
    [InlineData("TestCodeGenerator.Generator.Models", "TestCodeGenerator.Generator.Models", "")]
    [InlineData("TestCodeGenerator.Generator.Models.Info", "TestCodeGenerator.Generator.Models.Info", "")]
    [InlineData("TestCodeGenerator.Generator.Tests", "TestCodeGenerator.Generator.Tests.Models", "Models")]
    [InlineData("TestCodeGenerator.Tests", "TestCodeGenerator.Tests.Generator.Models", "Generator.Models")]
    public void Ctor_ShouldDetectNamespacesCorrectly(string outputRootNamespace, string expectedBuilderClassNmsp, 
        string expectedInAssemblyPath)
    {
        // Arrange
        var type = typeof(NamespaceInfo);

        // Act
        var sut = new NamespaceInfo(type, outputRootNamespace);

        // Assert
        sut.BuilderClassNamespace.Should().Be(expectedBuilderClassNmsp);
        sut.InAssemblyPath.Should().Be(expectedInAssemblyPath);
    }
}
