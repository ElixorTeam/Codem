using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Components.Profile;

public partial class SnippetsTableEntry : ComponentBase
{
    [Parameter, EditorRequired] public SnippetTableModel Snippet { get; set; } = null!;
}