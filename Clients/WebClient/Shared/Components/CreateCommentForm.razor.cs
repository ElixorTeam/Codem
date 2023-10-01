using Microsoft.AspNetCore.Components;
namespace WebClient.Shared.Components;

public partial class CreateCommentForm : ComponentBase
{
    private string Text { get; set; }
    

    public CreateCommentForm()
    {
        Text = string.Empty;
    }
}