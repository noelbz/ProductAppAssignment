using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using System.Threading.Tasks;

namespace BookApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        IJsonBookRepository repository = new JsonBookRepository("books.json");
        IProductService bookService = new BookService(repository);

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Meny");
            Console.WriteLine("1. Add a new book");
            Console.WriteLine("2. Show booklist");
            Console.WriteLine("3. Save the booklist");
            Console.WriteLine("4. Load the booklist");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("What is the Title of the book?");
                    var title = Console.ReadLine();
                    Console.WriteLine("Who is the Author of the book?");
                    var author = Console.ReadLine();
                    Console.WriteLine("What is the Price of the book?");
                    var priceInput = Console.ReadLine();
                    if(!decimal.TryParse(priceInput, out decimal price))
                    {
                        Console.WriteLine("Invalid price, press any key...");
                        Console.ReadKey();
                        break;
                    }
                    await bookService.AddBookAsync(title!, author!, price);
                    Console.WriteLine("Book successfully added, press any key...");
                    Console.ReadKey();
                    break;
                case "2":
                    await bookService.ShowAllBooksAsync();
                    Console.WriteLine("Press any key...");
                    Console.ReadKey();
                    break;
                case "3":
                    await bookService.SaveAllBooksToFileAsync();
                    Console.WriteLine("Books successfully saved, press any key...");
                    Console.ReadKey();
                    break;
                case "4":
                    await bookService.LoadAllBooksFromFileAsync();
                    Console.WriteLine("Books successfully loaded, press any key...");
                    Console.ReadKey();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}