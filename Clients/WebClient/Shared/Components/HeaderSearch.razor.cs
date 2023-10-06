using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Shared.Components;

public partial class HeaderSearch : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private string searchQuery = ""; 
    
    private void RedirectToSearch(KeyboardEventArgs e)
    {
        if (!(e.Code == "Enter" || e.Code == "NumpadEnter")) return;
        string url = "/search";
        if (!(string.IsNullOrEmpty(searchQuery))) 
            url = $"{url}?searchQuery={searchQuery}";
        searchQuery = String.Empty;
        NavigationManager.NavigateTo(url);
    }
}