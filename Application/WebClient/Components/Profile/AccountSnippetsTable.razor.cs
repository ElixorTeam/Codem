namespace WebClient.Components.Profile;

public partial class AccountSnippetsTable
{
    public List<SnippetTableModel> snippetsList { get; set; } = new();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        snippetsList = new List<SnippetTableModel>
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