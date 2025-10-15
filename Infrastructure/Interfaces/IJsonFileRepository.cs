using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IJsonFileRepository
{
    // Sparar en produktlista till JSON-filen...
    void SaveToFile(List<Product> products);
    // Returnerar en produktlista från JSON-filen...
    List<Product> LoadFromFile();
}
