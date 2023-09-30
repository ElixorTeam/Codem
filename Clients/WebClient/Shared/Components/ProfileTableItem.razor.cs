using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Shared.Components;

public partial class ProfileTableItem : ComponentBase
{
    [Parameter] public string Title { get; set; }
    [Parameter] public DateOnly PublicDate { get; set; }
    [Parameter] public string ExpireTime { get; set; }
    [Parameter] public int Views { get; set; }
    [Parameter] public int Stars { get; set; }
    [Parameter] public string ProgramLanguage { get; set; }
}