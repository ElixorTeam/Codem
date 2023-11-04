using Microsoft.AspNetCore.Components;

namespace WebClient.Components.Layouts;

public sealed partial class MainFooter : ComponentBase
{
    private string Incorporated => $"© {DateTime.Now.Year} Elixor, Inc.";
}