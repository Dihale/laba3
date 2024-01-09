namespace lab3;

public class StorageService
{
    private Storage _storage = new Storage();

    public StorageService(Storage storage)
    {
        _storage = storage;
    }

    public void SaveOrLoadMenu(List<QueryInformation> queries = null)
    {
        Console.WriteLine("Save or Load Menu:");
        Console.WriteLine("1 - Save to JSON");
        Console.WriteLine("2 - Load from JSON");
        Console.WriteLine("3 - Save to XML");
        Console.WriteLine("4 - Load from XML");
        Console.WriteLine("5 - Save to SQLite");
        Console.WriteLine("6 - Load from SQLite");
        Console.WriteLine("exit - Back to main menu");
        Console.Write("> ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                _storage.SaveToJson(queries);
                Console.WriteLine("Data saved to JSON.");
                break;
            case "2":
                var tasks = _storage.LoadFromJson();
                Console.WriteLine("Data loaded from JSON.");
                break;
            case "3":
                _storage.SaveToXml(queries);
                Console.WriteLine("Data saved to XML.");
                break;
            case "4":
                _storage.LoadFromXML();
                Console.WriteLine("Data loaded from XML.");
                break;
            case "5":
                _storage.SaveToSqLite(queries);
                Console.WriteLine("Data saved to SQLite.");
                break;
            case "6":
                _storage.LoadFromSqLite();
                Console.WriteLine("Data loaded from SQLite.");
                break;
            case "exit":
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}