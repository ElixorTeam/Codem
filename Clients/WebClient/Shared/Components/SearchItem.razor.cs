using Microsoft.AspNetCore.Components;

namespace WebClient.Shared.Components;

public partial class SearchItem : ComponentBase
{
    [Parameter] public int Id { get; set; }
    [Parameter] public string Title { get; set; } = String.Empty;
    [Parameter] public string Syntax { get; set; } = String.Empty;
    [Parameter] public string Author { get; set; } = String.Empty;
    [Parameter] public DateOnly PublicDate { get; set; }
    [Parameter] public string AvatarSrc { get; set; } = "assets/avatar.jpg";
    [Parameter] public string Code { get; set; } = String.Empty;
}