using Microsoft.AspNetCore.Components;

namespace WebClient.Components.Profile;

public sealed partial class AccountSnippetsTable: ComponentBase
{
    private List<SnippetTableModel> SnippetsList { get; set; } = new();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        SnippetsList = new List<SnippetTableModel>
        {
            new() {
                Title="Python code",
                PublicDate=DateOnly.FromDateTime(DateTime.Today),
                ExpireTime="1 week",
                Views=1400,
                Stars=100,
                ProgramLanguage="Python"
            },
        };
    }
}