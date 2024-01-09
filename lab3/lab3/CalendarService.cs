namespace lab3;

public class CalendarService
{
    private Calendar _calendar;
    private StorageService _storageService;
    private List<QueryInformation> _queries = new List<QueryInformation>();

    public CalendarService(Calendar calendar, StorageService storageService)
    {
        _calendar = calendar;
        _storageService = storageService;
    }

    public void RunCalendar()
    {
        Console.WriteLine("Welcome to the Calendar App!");
        Console.WriteLine("To use the app, enter one of the following commands:");
        Console.WriteLine("1 - Check if a year is a leap year");
        Console.WriteLine("2 - Calculate the duration between two dates");
        Console.WriteLine("3 - Get the day of the week for a given date");
        Console.WriteLine("4 - Save or load queries information");
        Console.WriteLine("Enter 'exit' to quit the program.");

        var command = "";
        while (command != "exit")
        {
            Console.Write("Enter a command: ");
            command = Console.ReadLine();

            switch (command)
            {
                case "1":
                    CheckLeapYear();
                    break;
                case "2":
                    CalculateDuration();
                    break;
                case "3":
                    GetDayOfWeek();
                    break;
                case "4":
                    _storageService.SaveOrLoadMenu(_queries);
                    break;
                case "exit":
                    Console.WriteLine("Exiting the program...");
                    return;
                default:
                    Console.WriteLine("Invalid command. Choose a command using numbers. Please try again.");
                    break;
            }
        }
    }
    
    private void CheckLeapYear()
    {
        var indexOperation = 1;
        var input = string.Empty; 
        var result = string.Empty; 
        
        Console.Write("Enter a year: ");
        if (int.TryParse(Console.ReadLine(), out var year))
        {
            input = year.ToString();
            if (_calendar.IsLeapYear(year))
                result = $"{year} is a leap year.";
            else
                result = ($"{year} is not a leap year.");
            Console.WriteLine(result);
        }
        else
        {
            result = "Incorrect data entry format. Please enter a valid year.";
            Console.WriteLine(result);
        }
        
        _queries.Add(new QueryInformation(indexOperation, input, result));
    }

    private void CalculateDuration()
    {
        var indexOperation = 2;
        var input = string.Empty; 
        var result = string.Empty; 
        Console.Write("Enter the first date (dd/MM/yyyy): ");
        var date1String = Console.ReadLine();

        Console.Write("Enter the second date (dd/MM/yyyy): ");
        var date2String = Console.ReadLine();

        input = date1String + "," + date2String;
        if (DateTime.TryParseExact(date1String, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None,
                out var date1)
            && DateTime.TryParseExact(date2String, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None,
                out var date2))
        {
            try
            {
                var duration = _calendar.CalculateDuration(date1, date2);
                result =  $"The duration between {date1.ToShortDateString()} and {date2.ToShortDateString()} is {duration} days.";
                Console.WriteLine(result);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            result = "Invalid date format. Please try again.";
            Console.WriteLine(result);
        }
        
        _queries.Add(new QueryInformation(indexOperation, input, result));
    }

    private void GetDayOfWeek()
    {
        var indexOperation = 3;
        var result = string.Empty; 
        Console.Write("Enter a date (dd/MM/yyyy): ");
        var input = Console.ReadLine();

        if (DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None,
                out var date))
        {
            result = $"The day of the week for {date.ToShortDateString()} is {_calendar.GetDayOfWeek(date)}.";
            Console.WriteLine(result);
        }
        else
        {
            result = "Invalid date format. Please try again.";
            Console.WriteLine(result);
        }
        
        _queries.Add(new QueryInformation(indexOperation, input, result));
    }
    
    
}