using Infrastructure.Models;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System.Text.Json;

namespace Infrastructure.Services;
// Logik för bokhantering.
public class ProductService : IProductService
{
    // Depndency Injection av IJsonBookRepository för att kunna använda dess metoder kring filhanteringen.
    private readonly IJsonFileRepository _repository;
    // Skapar en lista för böcker.
    private readonly List<Product> _products;
    // Konstruktor: Bokservice skapas med repository som parameter.
    public ProductService(IJsonFileRepository repository)
    {
        // Parametern sätts till den privata fältet och kan användas i klassen.
        _repository = repository;
        // Initierar boklistan med produkterna från JSON filen redan vid START.
        _products = _repository.LoadFromFile();
    }
    // Metod som ska lägga till bok utifrån titel, författare och pris.
    public void AddProduct(string name, decimal price)
    {
        // Om titel eller författarens namn är null eller mellanslag.
        if (string.IsNullOrWhiteSpace(name))
        {
            // Skriv felmeddelande.
            Console.WriteLine("Namnet kan inte vara tomt eller mellanslag.");
            return;
        }
        // Om priset är negativt.
        if (price < 0)
        {
            // Skriv felmeddelande.
            Console.WriteLine("Priset kan inte vara negativt.");
            return;
        }
        // Skapar en bok om egenskaperna klarar valideringen.
        var product = new Product
        {
            Name = name,
            Price = price
        };
        // Lägger även till boken i boklistan.
        _products.Add(product);
        Console.WriteLine($"Du har lagt till produkten '{name}'.");
    }
    // Metod som visar alla böcker i boklistan.
    public void ShowProducts()
    {
        // Om boklistan har inga böcker.
        if (_products.Count == 0)
        {
            // Skriv meddelande att inga böcker finns.
            Console.WriteLine("Inga produkter tillgängliga!");
            return;
        }
        // Om det finns böcker dock, så skriv ut dom baserad på deras egenskaper.
        foreach (var product in _products)
        {
            Console.WriteLine($"ID: {product.Id}\nName: {product.Name}\nPris: {product.Price}");
        }
    }
    // Metod som sparar alla böcker till JSON-fil.
    public void SaveProductsToFile()
    {
        // Repository metod som skriver boklistan till filströmmen,
        // som skickas vidare till JSON-filen.
        _repository.SaveToFile(_products);
        Console.WriteLine("Produkter sparade till fil!");
    }
    // Metod som laddar alla böcker från JSON-fil.
    public void LoadProductsFromFile()
    {
        // Metod som gör JSON filen till en produktlista,
        // listan sparas i "loaded".
        var loaded = _repository.LoadFromFile();
        // Om listan som laddats från JSON-filen har produkter i sig.
        if (loaded.Count > 0)
        {
            // Så rensas produkterna från gamla listan.
            _products.Clear();
            // Och produkterna som laddats in från JSON-filen läggs in istället.
            _products.AddRange(loaded);
            Console.WriteLine("Produkter har laddats in från fil!");
        }
        // Om 
        else
        {
            Console.WriteLine("Inga produkter hittades i filen.");
        }
    }
    //för testandet
    public List<Product> GetProducts()
    {
        return _products;
    }

}
