using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebClient.Components.CodeEditor;

public sealed partial class SelectLanguage: ComponentBase
{
    [Parameter] public string ActiveLanguage { get; set; } = "Markdown";
    [Parameter] public EventCallback<string> ActiveLanguageChanged { get; set; }
    
    [Inject] private IJSRuntime JSRuntime { get; set; }
    private IJSObjectReference? _module;
    private string SearchString { get; set; } = string.Empty;

    private List<string> Languages { get; } = new() {
        "C", "C++", "CSS", "HTML", "Java", "JavaScript", "JSON", "JSX", "MariaDB SQL", "Markdown", "MS SQL",
        "MySQL", "PHP", "PostgreSQL", "Python", "Rust", "SQL", "SQLite", "TSX", "TypeScript", "XML", "C#", 
        "CMake", "Cython", "Dart", "Dockerfile", "Erlang", "Fortran", "F#", "Go", "Groovy", "Haskell", "HTTP", 
        "Jinja2", "Kotlin", "LESS", "Lua", "Nginx", "Objective-C", "Objective-C++", "Pascal", "Perl", "PowerShell", 
        "Ruby", "Sass", "SCSS", "Shell", "Swift", "sTeX", "LaTeX", "TOML", "VB.NET", "YAML",
    };
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/langDropdown.js");
            await _module.InvokeVoidAsync("initLangDropdown");
        }
    }

    private async Task HideDropdown()
    {
        if (_module == null) return;
        await _module.InvokeVoidAsync("hideLangDropdown");
    }
    
    private void FilterItems(ChangeEventArgs e) => SearchString = e.Value.ToString();
    private async void SwitchLanguage(string lang)
    {
        ActiveLanguage = lang;
        await ActiveLanguageChanged.InvokeAsync(lang);
        await HideDropdown();
    }
}