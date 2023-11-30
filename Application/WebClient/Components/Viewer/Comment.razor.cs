using Microsoft.AspNetCore.Components;

namespace WebClient.Components.Viewer;

public partial class Comment : ComponentBase
{
    [Parameter] public string Author { get; set; } = string.Empty;
    [Parameter] public DateOnly CreateDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [Parameter] public string AvatarSrc { get; set; } = string.Empty;
    [Parameter] public string CommentText { get; set; } = string.Empty;
}