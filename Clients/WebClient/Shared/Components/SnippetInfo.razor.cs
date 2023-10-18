using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Shared.Components;

public sealed partial class SnippetInfo : ComponentBase
{
    [Parameter] public string Author { get; set; }
    [Parameter] public string Title { get; set; }
    [Parameter] public DateOnly CreateDate { get; set; }
    [Parameter] public string Description { get; set; }
    [Parameter] public string ProgramLanguage { get; set; } 
    [Parameter] public string Views { get; set; }
    [Parameter] public string Stars { get; set; }
    [Parameter] public string Access { get; set; }

    public SnippetInfo()
    {
        Description = string.Empty;
        ProgramLanguage = string.Empty;
        Views = string.Empty;
        Stars = string.Empty;
        Access = string.Empty;
    }
}