using Microsoft.AspNetCore.Components;

namespace WebClient.Components.Viewer;

public partial class Comment : ComponentBase
{
    [Parameter] public string Author { get; set; } = String.Empty;
    [Parameter] public DateOnly CreateDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [Parameter] public string AvatarSrc { get; set; } = String.Empty;
    [Parameter] public string CommentText { get; set; } = String.Empty;
}