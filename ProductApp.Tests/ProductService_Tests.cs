using System.IO;
using System.Linq;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Models;
using Infrastructure.Services;
namespace ProductApp.Tests;

public class ProductService_Tests
{
    private readonly string _testFilePath = "test_products.json";
    // Konstruktor som kör varje test för att börja rent.
    // (om json filen existerar så tas den bort)
    public ProductService_Tests()
    {
        if (File.Exists(_testFilePath))
            File.Delete(_testFilePath);
    }

    [Fact]
    // Testar att produkterna läggs till i listan,
    // om de görs det så returnera sant.
    public void AddProductToList_ShouldAddProductToList_ReturnTrue()
    {
        // Arrange - förberedelser
        var repo = new JsonFileRepository("test_products.json");
        var productService = new ProductService(repo);
        // Act - utförandet
        productService.AddProduct("Testprodukt", 1000M);
        var products = productService.GetProducts();
        // Assert - result / utfall
        // Ska finnas en produkt.
        Assert.Single(products);
        Assert.Equal("Testprodukt", products[0].Name);
        Assert.Equal(1000M, products[0].Price);
        // Guid kan inte vara tom.
        Assert.NotEqual(System.Guid.Empty, products[0].Id);
    }
    public void SaveAndLoadProductsToList_ShouldPersistData()
    {
        // Arrange - Förbered instanserna och det du behöver för att testa.
        var repo = new JsonFileRepository(_testFilePath);
        var productService = new ProductService(repo);
        // Act
        productService.AddProduct("Kaffe", 30.90M);
        productService.SaveProductsToFile();

        // Ny service för att simulera en ny användarsession.
        var newService = new ProductService(repo);
        newService.LoadProductsFromFile();
        var loaded = productService.GetProducts();
        // Assert
        Assert.Single(loaded);
        Assert.Equal("Kaffe", loaded[0].Name);
        Assert.Equal(30.90M, loaded[0].Price);
    }
    public void AddProduct_ShouldRejectInvalidInput()
    {
        // 
        var repo = new JsonFileRepository(_testFilePath);
        var productService = new ProductService(repo);


    }
}
