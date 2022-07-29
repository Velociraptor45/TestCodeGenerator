// See https://aka.ms/new-console-template for more information

using Generator;
using Generator.Files;

new TestBuilderGenerator(new FileHandler()).Generate(
    @"ShoppingList.Api.Domain\bin\Debug\net6.0\ShoppingList.Api.Domain.dll",
    "Item",
    @"E:\Personal\Downloads");

// 