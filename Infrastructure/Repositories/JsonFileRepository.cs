using Infrastructure.Interfaces;
using Infrastructure.Models;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Infrastructure.Repositories;
public class JsonFileRepository : IJsonFileRepository
{
    // Här så skapas filvägen.
    private readonly string _filePath;
    // Här så skapas alternativen för JSON formatet som vi kan använda oss av.
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        // Indragen text, mer lättläst och snyggare ut.
        WriteIndented = true
    };
    // Konstruktor skapas bara om JSON-filnamnet nämns i parametern.
    public JsonFileRepository(string filename = "products.json")
    {
        // Sparar katalogen som applikationen körs i till "baseDir" variabeln.
        var baseDir = AppContext.BaseDirectory;
        // Skapar en "Data"-Mapp i katalogen och sparar till "dataDir" variabeln.
        var dataDir = Path.Combine(baseDir, "Data");
        // Skapar fullständiga filväg genom att slå ihop alla sökvägar:
        // "Base"-Katalogen > "Data"-Mappen > JSON-Filen via "filnamnet".
        _filePath = Path.Combine(dataDir, filename);
        // Metod som säkerställer att filvägen och "data"-mappen finns.
        EnsureInitialized(_filePath, dataDir);
    }
    // Skapar en metod för att säkerställa att mappen och filvägen blivit skapad.
    // Behöver en filväg och "data"-map för att fungera.
    public static void EnsureInitialized(string filePath, string dataDir)
    {
        // Om inte "Data"-Mappen finns.
        if (!Directory.Exists(dataDir))
            // Skapa "Data"-Mappen.
            Directory.CreateDirectory(dataDir);
        // Om inte filen existerar som finns på angivna filväg.
        if (!File.Exists(filePath))
            // Skapa filen på angivna filväg och skriv en tom array till den "[]".
            File.WriteAllText(filePath, "[]");
    }

    public void SaveToFile(List<Product> products)
    {
        // Gör om produktlistan till JSON innehåll
        var json = JsonSerializer.Serialize(products, _jsonOptions);
        // Skriver filen till min filepath.
        File.WriteAllText(_filePath, json);
    }

    public List<Product> LoadFromFile()
    {
        if (!File.Exists(_filePath))
            return new List<Product>();
        // JSON innehållet
        var json = File.ReadAllText(_filePath);
        // Gör om JSON innehållet till en produktlista 
        var products = JsonSerializer.Deserialize<List<Product>>(json);
        // Returnerar produktlistan om den finns, annars blir det en tom lista.
        return products ?? new List<Product>();
    }
}
