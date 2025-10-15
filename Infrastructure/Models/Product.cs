namespace Infrastructure.Models;

public class Product
{
    // Guid sätts automatisk om det inte finns id.
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
