using System.ComponentModel;

namespace Сodem.Shared.Enums;

public enum SnippetVisibilityEnum
{
    [Description("Public")]
    Public,
    [Description("Private")]
    Private,
    [Description("Shared by link")]
    ByLink,
}