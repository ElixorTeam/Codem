using Microsoft.AspNetCore.Components;
using WebClient.Models;
using Сodem.Shared.Enums;

namespace WebClient.Components.Forms;

public sealed partial class SnippetFormInputs
{
    [Parameter, EditorRequired] public CodeSnippetModel Model { get; set; } = null!;
    [Parameter] public bool IsUpdate { get; set; }
    private bool IsPasswordVisible { get; set; } = false;
    private SnippetExpiration SnippetExpiration { get; set; } = SnippetExpiration.Never;
    private static Array VisibilityList { get; } = Enum.GetValues(typeof(SnippetVisibilityEnum));
    private static Array ExpireTimeList { get; } = Enum.GetValues(typeof(SnippetExpiration));

    private void SwitchPasswordVisibility() => IsPasswordVisible = !IsPasswordVisible;
    
    private void HandleVisibilityChange()
    {
        if (!(Model.Visibility != SnippetVisibilityEnum.ByLink & !string.IsNullOrEmpty(Model.Password))) return;
        Model.Password = string.Empty;
        IsPasswordVisible = false;
    }

    private void OnExpireTimeChange()
    {
        if (SnippetExpiration == SnippetExpiration.Never)
        {
            Model.ExpireTime = DateOnly.FromDateTime(DateTime.MaxValue);
            return;
        }
        Model.ExpireTime = DateOnly.FromDateTime(DateTime.Now.Add(SnippetExpiration.ToTimeSpan()));
    }
     
}