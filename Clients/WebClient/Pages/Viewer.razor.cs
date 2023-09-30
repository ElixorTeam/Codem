using Microsoft.AspNetCore.Components;

namespace WebClient.Pages;

public partial class Viewer : ComponentBase
{
    [Parameter] public int Id { get; set; }
}