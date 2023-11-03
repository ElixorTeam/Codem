using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Components;

public sealed partial class ProfileTableItem : ComponentBase
{
    [Parameter] public string Title { get; set; }
    [Parameter] public string ExpireTime { get; set; }
    [Parameter] public string ProgramLanguage { get; set; }    
    [Parameter] public DateOnly PublicDate { get; set; }
    [Parameter] public int Views { get; set; }
    [Parameter] public int Stars { get; set; }
    
    public ProfileTableItem()
    {
        ExpireTime = string.Empty;
        Title = string.Empty;
        ProgramLanguage = string.Empty;
    }
}