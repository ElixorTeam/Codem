using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebClient.Components.CodeEditor;

public sealed partial class SelectLanguage: ComponentBase
{
    [Parameter] public string ActiveLanguage { get; set; } = "Markdown";
    [Parameter] public EventCallback<string> ActiveLanguageChanged { get; set; }

    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    private IJSObjectReference Module { get; set; } = null!;
    public string SearchString { get; set; } = string.Empty;

    private static List<string> Languages { get; } = new() {
        "C", "C++", "CSS", "HTML", "Java", "JavaScript", "JSON", "JSX", "MariaDB SQL", "Markdown", "MS SQL",
        "MySQL", "PHP", "PostgreSQL", "Python", "Rust", "SQL", "SQLite", "TSX", "TypeScript", "XML", "C#", 
        "CMake", "Cython", "Dart", "Dockerfile", "Erlang", "Fortran", "F#", "Go", "Groovy", "Haskell", "HTTP", 
        "Jinja2", "Kotlin", "LESS", "Lua", "Nginx", "Objective-C", "Objective-C++", "Pascal", "Perl", "PowerShell", 
        "Ruby", "Sass", "SCSS", "Shell", "Swift", "sTeX", "LaTeX", "TOML", "VB.NET", "YAML",
    };
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/CodeEditor/SelectLanguage.razor.js");
        await Module.InvokeVoidAsync("initLangDropdown");
    }

    private async Task HideDropdown() => await Module.InvokeVoidAsync("hideLangDropdown");

    private void ClearSearch() => SearchString = string.Empty;

    private void UpdateSearchString(ChangeEventArgs e) => SearchString = e.Value?.ToString() ?? string.Empty;
    
    private async void SwitchLanguage(string lang)
    {
        ActiveLanguage = lang;
        await ActiveLanguageChanged.InvokeAsync(lang);
        await HideDropdown();
        ClearSearch();
        StateHasChanged();
    }
}