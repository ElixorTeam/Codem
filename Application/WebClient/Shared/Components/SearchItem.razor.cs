using Microsoft.AspNetCore.Components;

namespace WebClient.Shared.Components;

public sealed partial class SearchItem : ComponentBase
{
    [Parameter] public int Id { get; set; }
    [Parameter] public DateOnly PublicDate { get; set; }
    [Parameter] public string Title { get; set; }
    [Parameter] public string Syntax { get; set; }
    [Parameter] public string Author { get; set; }
    [Parameter] public string AvatarSrc { get; set; }
    [Parameter] public string Code { get; set; }

    public SearchItem()
    {
        Title = string.Empty;
        Syntax = string.Empty;
        Author = string.Empty;
        AvatarSrc = "assets/avatar.jpg";
        Code = string.Empty;
    }
}