using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using System.Threading.Tasks;

namespace ProductApp;

public class Program
{
    public static void Main(string[] args)
    {
        IJsonFileRepository repository = new JsonFileRepository("products.json");
        IProductService productService = new ProductService(repository);
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("\n--- MENY ---");
            Console.WriteLine("1. Lägg till en produkt");
            Console.WriteLine("2. Visa produktlistan");
            Console.WriteLine("3. Spara produkterna till fil");
            Console.WriteLine("4. Avsluta");
            Console.Write("Välj: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Ange produktens namn: ");
                    var name = Console.ReadLine();
                    Console.WriteLine("Ange produktens pris:");
                    // Skapa decimal variabel.
                    decimal price;
                    // Om inmatningen är skriven i decimaltyp,
                    // så konverteras den till price variabeln.
                    if (decimal.TryParse(Console.ReadLine(), out price))
                        // Om inmatningen blev decimal så läggs den till i listan.
                        productService.AddProduct(name!, price);
                    // Om konverteringen inte funkar blir det fel.
                    else
                        Console.WriteLine("Fel, skriv siffror...");
                        break;
                case "2":
                    productService.ShowProducts();
                    Console.WriteLine("Tryck valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    break;
                case "3":
                    productService.SaveProductsToFile();
                    Console.WriteLine("Tryck valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Välj mellan 1-4 i menyn...");
                    Console.WriteLine("Tryck valfri tangent för att fortsätta...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}