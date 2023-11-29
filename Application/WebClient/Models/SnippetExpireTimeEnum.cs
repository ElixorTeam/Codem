using System.ComponentModel;

namespace Сodem.Shared.Enums;

public enum SnippetExpiration
{
    [Description("1 hour")]
    OneHour,
    [Description("1 day")]
    OneDay,
    [Description("1 week")]
    OneWeek,
    [Description("1 month")]
    OneMonth,
    [Description("Never")]
    Never,
}

public static class SnippetExpirationExtensions
{
    public static TimeSpan ToTimeSpan(this SnippetExpiration expiration)
    {
        return expiration switch
        {
            SnippetExpiration.Never => TimeSpan.FromDays(365 * 100),
            SnippetExpiration.OneHour => TimeSpan.FromHours(1),
            SnippetExpiration.OneDay => TimeSpan.FromDays(1),
            SnippetExpiration.OneWeek => TimeSpan.FromDays(7),
            SnippetExpiration.OneMonth => TimeSpan.FromDays(30),
            _ => TimeSpan.Zero
        };
    }
}