using Microsoft.AspNetCore.Components;

namespace WebClient.Components;

public sealed partial class CreateCommentForm : ComponentBase
{
    private string Text { get; set; } = string.Empty;
}