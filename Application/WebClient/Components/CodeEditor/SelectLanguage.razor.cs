using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebClient.Models;
using WebClient.Utils;

namespace WebClient.Components.CodeEditor;

public sealed partial class SelectLanguage: ComponentBase
{
    [Parameter] public ProgrammingLanguage ActiveLanguage { get; set; } = ProgrammingLanguage.Markdown;
    [Parameter] public EventCallback<ProgrammingLanguage> ActiveLanguageChanged { get; set; }

    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    private IJSObjectReference Module { get; set; } = null!;
    public string SearchString { get; set; } = string.Empty;
    private string _dropdownUniqueId = Guid.NewGuid().ToString();

    private static IEnumerable<ProgrammingLanguage> Languages { get; } =
        Enum.GetValues(typeof(ProgrammingLanguage)).Cast<ProgrammingLanguage>();
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/dropdownInterface.js");
        await Module.InvokeVoidAsync("initDropdown", "langDropdown",
            "langDropdownButton", _dropdownUniqueId);
    }

    private async void InitDropdown()
    {
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/dropdownInterface.js");
        var options = new { offsetSkidding = -40 };
        await Module.InvokeVoidAsync("initDropdown", "langDropdown",
            "langDropdownButton", _dropdownUniqueId, options);
    }
    
    private IEnumerable<ProgrammingLanguage> FilteredLanguages()
    {
        if (string.IsNullOrEmpty(SearchString)) 
            return Enum.GetValues(typeof(ProgrammingLanguage)).Cast<ProgrammingLanguage>();
        
        return Enum.GetValues(typeof(ProgrammingLanguage))
            .Cast<ProgrammingLanguage>()
            .Where(LanguageMatchesSearch);
    }
    
    private bool LanguageMatchesSearch(ProgrammingLanguage language)
    {
        string description = EnumHelper.GetEnumDescription(language);
        return description.Contains(SearchString, StringComparison.OrdinalIgnoreCase);
    }

    private async Task HideDropdown() => await Module.InvokeVoidAsync("hideLangDropdown", _dropdownUniqueId);

    private void ClearSearch() => SearchString = string.Empty;

    private void UpdateSearchString(ChangeEventArgs e) => SearchString = e.Value?.ToString() ?? string.Empty;
    
    private async void SwitchLanguage(ProgrammingLanguage lang)
    {
        ActiveLanguage = lang;
        await ActiveLanguageChanged.InvokeAsync(lang);
        await HideDropdown();
        ClearSearch();
        StateHasChanged();
    }
}