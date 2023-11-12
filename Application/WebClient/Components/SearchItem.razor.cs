using Microsoft.AspNetCore.Components;

namespace WebClient.Components;

public sealed partial class SearchItem : ComponentBase
{
    [Parameter] public Guid Id { get; init; }
    [Parameter] public DateOnly PublicDate { get; init; } = DateOnly.FromDateTime(DateTime.Now);
    [Parameter] public string Title { get; init; } = string.Empty;
    [Parameter] public string Syntax { get; init; } = string.Empty;
    [Parameter] public string Author { get; init; } = string.Empty;
    [Parameter] public string AvatarSrc { get; init; } = "assets/avatar.jpg";
    [Parameter] public string Code { get; init; } = string.Empty;
}