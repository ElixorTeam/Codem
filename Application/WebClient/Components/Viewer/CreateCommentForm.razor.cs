using Microsoft.AspNetCore.Components;

namespace WebClient.Components.Viewer;

public sealed partial class CreateCommentForm : ComponentBase
{
    private string Text { get; set; } = string.Empty;
}