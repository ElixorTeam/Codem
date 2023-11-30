namespace WebClient.Utils;

public static class DateFormatting
{
    public static string FormatDateDifference(DateOnly targetDate)
    {
        TimeSpan difference = targetDate.ToDateTime(TimeOnly.MinValue) - DateTime.Today;
        return difference.TotalDays switch
        {
            < 60 => $"{(int)difference.TotalDays} days",
            < 730 => $"{(int)(difference.TotalDays / 30)} months",
            < 10000 => $"{(int)(difference.TotalDays / 365)} years",
            _ => "Never"
        };
    }
}