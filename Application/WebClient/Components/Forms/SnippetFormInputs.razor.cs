using Microsoft.AspNetCore.Components;
using WebClient.Models;
using Сodem.Shared.Enums;

namespace WebClient.Components.Forms;

public sealed partial class SnippetFormInputs
{
    [Parameter, EditorRequired] public CodeSnippetModel Model { get; set; } = null!;
    [Parameter] public bool IsAuthorized { get; set; } = true;
    [Parameter] public bool IsUpdate { get; set; }
    private bool IsPasswordVisible { get; set; } = false;
    private SnippetExpiration SnippetExpiration { get; set; } = SnippetExpiration.OneWeek;
    private static List<SnippetVisibilityEnum> VisibilityList { get; set; } = new();
    private static List<SnippetExpiration> ExpireTimeList { get; set; } = new();

    protected override void OnInitialized()
    {
        VisibilityList = Enum.GetValues(typeof(SnippetVisibilityEnum)).Cast<SnippetVisibilityEnum>().ToList();
        ExpireTimeList = Enum.GetValues(typeof(SnippetExpiration)).Cast<SnippetExpiration>().ToList();
        if (IsAuthorized) return;
        ExpireTimeList = ExpireTimeList.Where(c => c != SnippetExpiration.Never).ToList();
        VisibilityList = VisibilityList.Where(c => c != SnippetVisibilityEnum.Private).ToList();
    }

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