using Microsoft.AspNetCore.Components;

namespace WebClient.Pages;

public sealed partial class Viewer : ComponentBase
{
    [Parameter] public int Id { get; set; }
}