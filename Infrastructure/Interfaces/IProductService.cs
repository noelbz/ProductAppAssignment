
using Infrastructure.Models;

namespace Infrastructure.Interfaces;
// Logiken för hur böcker hanteras.
public interface IProductService
{
    // Metod som returnerar en lista produkter (Används för enhetstestandet).
    List<Product> GetProducts();
    // Metod som sätter namn och pris för att skapa en produkt.
    void AddProduct(string name, decimal price);
    // Visa produkterna.
    void ShowProducts();
    // Spara produkterna till JSON filen.
    void SaveProductsToFile();
    // Ladda produkter från JSON filen.
    void LoadProductsFromFile();
}
