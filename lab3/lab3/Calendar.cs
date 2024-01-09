namespace lab3;

public class Calendar
{
    public bool IsLeapYear(int year)
    {
        return DateTime.IsLeapYear(year);
    }

    public int CalculateDuration(DateTime date1, DateTime date2)
    {
        if (date1 > date2)
        {
            throw new ArgumentException("The first date should be earlier than the second date.");
        }

        var duration = date2 - date1;
        return (int)duration.TotalDays;
    }

    public string GetDayOfWeek(DateTime date)
    {
        return date.DayOfWeek.ToString();
    }
}