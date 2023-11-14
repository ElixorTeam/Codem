using Microsoft.AspNetCore.Components;
using WebClient.Models;

namespace WebClient.Components.Forms;

public sealed partial class SnippetFormInputs
{
    [Parameter, EditorRequired] public CodeSnippetModel Model { get; set; } = null!;
    
    private bool IsPasswordVisible { get; set; } = false;
    private static Array ExpireTimeList { get; } = Enum.GetValues(typeof(SnippetExpiration));

    private void SwitchPasswordVisibility() => IsPasswordVisible = !IsPasswordVisible;
    
    private void HandleIsPrivateChange()
    {
        if (Model.IsPrivate & !string.IsNullOrEmpty(Model.Password))
            Model.Password = string.Empty;
    }
}