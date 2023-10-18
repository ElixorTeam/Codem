using Microsoft.AspNetCore.Components;
namespace WebClient.Shared.Layouts;

public sealed partial class MainFooter : ComponentBase
{
    private string Incorporated => $"© {DateTime.Now.Year} Elixor, Inc.";
}