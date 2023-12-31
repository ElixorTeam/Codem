using System.ComponentModel;

namespace WebClient.Models;

public enum SnippetExpiration
{
    [Description("1 day")]
    OneDay,
    [Description("1 week")]
    OneWeek,
    [Description("1 month")]
    OneMonth,
    [Description("1 year")]
    OneYear,
    [Description("Never")]
    Never,
}

public static class SnippetExpirationExtensions
{
    public static TimeSpan ToTimeSpan(this SnippetExpiration expiration)
    {
        return expiration switch
        {
            SnippetExpiration.OneDay => TimeSpan.FromDays(1),
            SnippetExpiration.OneWeek => TimeSpan.FromDays(7),
            SnippetExpiration.OneMonth => TimeSpan.FromDays(30),
            SnippetExpiration.OneYear => TimeSpan.FromDays(365),
            _ => TimeSpan.Zero
        };
    }
}