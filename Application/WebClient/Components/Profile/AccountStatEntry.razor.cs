using Microsoft.AspNetCore.Components;

namespace WebClient.Components.Profile;

public partial class AccountStatEntry
{
    [Parameter, EditorRequired] public StatModel StatModel { get; set; } = null!;
}