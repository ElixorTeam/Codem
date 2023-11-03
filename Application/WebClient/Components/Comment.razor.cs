using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebClient.Components;

public sealed partial class Comment : ComponentBase
{
    [Parameter] public string Author { get; set; }
    [Parameter] public DateOnly CreateDate { get; set; }
    [Parameter] public string AvatarSrc { get; set; }
    [Parameter] public string CommentText { get; set; }

    public Comment()
    {
        AvatarSrc = string.Empty;
        CommentText = string.Empty;
    }
}