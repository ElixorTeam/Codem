using Microsoft.AspNetCore.Components;

namespace WebClient.Shared.Icons;

public partial class CodeBreakIcon : ComponentBase
{
    [Parameter] public string Class { get; set; } = string.Empty;
}