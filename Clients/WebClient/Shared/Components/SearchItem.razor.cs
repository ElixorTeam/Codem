using Microsoft.AspNetCore.Components;

namespace WebClient.Shared.Components;

public partial class SearchItem : ComponentBase
{
    [Parameter] public string id { get; set; }
    [Parameter] public string Title { get; set; }
    [Parameter] public string Syntax { get; set; }
    [Parameter] public string Author { get; set; }
    [Parameter] public DateOnly PublicDate { get; set; }
    [Parameter] public string AvatarSrc { get; set; } = "assets/avatar.jpg";
    [Parameter] public string Code { get; set; }
}