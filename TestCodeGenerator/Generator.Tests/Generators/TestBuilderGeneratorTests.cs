using FluentAssertions;
using RefleCS;
using System.Reflection;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Generators;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;
using TestCodeGenerator.TestTools;
using TestCodeGenerator.TestTools.Exceptions;

namespace TestCodeGenerator.Generator.Tests.Generators;

public class TestBuilderGeneratorTests
{
    private readonly TestBuilderGeneratorFixture _fixture;

    public TestBuilderGeneratorTests()
    {
        _fixture = new TestBuilderGeneratorFixture();
    }

    public static IEnumerable<object[]> GenerateTestData()
    {
        yield return new object[] { nameof(IntTest), IntTest.GetExpectedBuilder() };
        yield return new object[] { nameof(NullableIntTest), NullableIntTest.GetExpectedBuilder() };
        yield return new object[] { nameof(BoolTest), BoolTest.GetExpectedBuilder() };
        yield return new object[] { nameof(NullableEnumerableTest), NullableEnumerableTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(NullableEnumerableWithNullableArgTest), NullableEnumerableWithNullableArgTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(ListTest), ListTest.GetExpectedBuilder() };
        yield return new object[] { nameof(DictionaryTest), DictionaryTest.GetExpectedBuilder() };
        yield return new object[] { nameof(EnumerableTest), EnumerableTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(InheritFromIEnumerableWithStandardCtorTest),
            InheritFromIEnumerableWithStandardCtorTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(InheritFromIEnumerableWithoutStandardCtorTest),
            InheritFromIEnumerableWithoutStandardCtorTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(NullableDictionaryTest), NullableDictionaryTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(NullableDictionaryWithSecondArgNullableTest),
            NullableDictionaryWithSecondArgNullableTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(SingleGenericTest), SingleGenericTest.GetExpectedBuilder() };
        yield return new object[] { nameof(DoubleGenericTest), DoubleGenericTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(NullableEnumerableInEnumerableTest), NullableEnumerableInEnumerableTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(EnumerableInEnumerableTest), EnumerableInEnumerableTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(ListInEnumerableTest), ListInEnumerableTest.GetExpectedBuilder() };
        yield return new object[] { nameof(EnumerableInListTest), EnumerableInListTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(DoubleGenericInEnumerableTest), DoubleGenericInEnumerableTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(SingleGenericInDoubleGenericTest), SingleGenericInDoubleGenericTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(DuplicatedCtorParametersTest), DuplicatedCtorParametersTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(DuplicatedCtorParameterTypesTest), DuplicatedCtorParameterTypesTest.GetExpectedBuilder()
        };
    }

    [Theory]
    [MemberData(nameof(GenerateTestData))]
    public void Generate_WithIntCtor_ShouldSaveExpectedResult(string className, string expectedBuilder)
    {
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            var filePath = Path.Combine(folderPath, $"{className}Builder.cs");
            _fixture.SetupBuilderConfiguration(folderPath);
            _fixture.SetupFileHandlerLoadingAssembly();
            var sut = _fixture.CreateSut();

            // Act
            sut.Generate(className);

            // Assert
            File.Exists(filePath).Should().BeTrue();

            var fileContent = File.ReadAllText(filePath);
            fileContent.Should().Be(expectedBuilder);
        });
    }

    private class TestBuilderGeneratorFixture
    {
        private readonly Mock<IFileHandler> _fileHandlerMock = new(MockBehavior.Strict);
        private BuilderConfiguration? _builderConfiguration;
        private readonly Assembly _assembly;

        public TestBuilderGeneratorFixture()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public TestBuilderGenerator CreateSut()
        {
            TestPropertyNotSetException.ThrowIfNull(_builderConfiguration);

            return new TestBuilderGenerator(_fileHandlerMock.Object, new CsFileHandler(), _builderConfiguration);
        }

        public void SetupBuilderConfiguration(string outputFolder)
        {
            _builderConfiguration = new BuilderConfiguration
            {
                DllPath = "test.Path.dll",
                OutputFolder = outputFolder,
                GenericSuperclassTypeName = "DomainTestBuilderBase",
                GenericSuperclassNamespace = "Superclass.Namespace",
                CtorInjectionMethodName = "FillConstructorWith",
                OutputAssemblyRootNamespace = "TestCodeGenerator.Generator.Tests.Tests"
            };
        }

        public void SetupFileHandlerLoadingAssembly()
        {
            TestPropertyNotSetException.ThrowIfNull(_builderConfiguration);

            _fileHandlerMock.Setup(m => m.LoadAssembly(_builderConfiguration.DllPath))
                .Returns(_assembly);
        }
    }
}