using Xunit;
using System.IO;
using System.Linq;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Models;
using Infrastructure.Services;
namespace Infrastructure.Tests;

public class ProductService_Tests
{


    [Fact]
    // Enhetstest som kollar att det går att lägga in produkter i listan.
    public void AddProductToList_ShouldAddProductToList_ReturnTrue()
    {
        // Arrange - Gör alla förberedelser.
        // Skapar en filväg till en ny JSON fil som är menat för testet.
        var testFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "test_products.json");
        // Om fil på filvägen finns...
        if (File.Exists(testFilePath))
            // Filen tas bort.
            File.Delete(testFilePath);
        // Skapar en instans av servicen och repositoryt, för att använda productservice metoder
        IJsonFileRepository testRepo = new JsonFileRepository("test_products.json");
        IProductService productService = new ProductService(testRepo);

        // Act - Utförandet (Kör funktionen som jag vill testa)
        productService.AddProduct("Iphone 17", 10995);
        var products = productService.GetProducts();

        // Assert - Resultatet (Kontrollerar att jag fick det resultatet som jag förvantade mig).
        // Det ska bara finnas en produkt i produktlistan.
        Assert.Single(products);
        // Produktens namns ska vara Iphone 17.
        Assert.Equal("Iphone 17", products[0].Name);
        // Produktens pris ska vara 10995.
        Assert.Equal(10995, products[0].Price);
    }
}
