namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var calendar = new Calendar();
            var storage = new Storage();
            
            var storageService = new StorageService(storage);

            var calendarService = new CalendarService(calendar, storageService);

            calendarService.RunCalendar();
        }
    }
}