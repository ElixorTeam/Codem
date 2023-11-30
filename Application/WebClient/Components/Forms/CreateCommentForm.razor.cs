using Microsoft.AspNetCore.Components;

namespace WebClient.Components.Forms;

public sealed partial class CreateCommentForm : ComponentBase
{
    private string Text { get; set; } = string.Empty;
}