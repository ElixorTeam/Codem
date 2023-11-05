using System.Linq;
using Microsoft.AspNetCore.Components;

namespace WebClient.Components.CodeEditor;

public sealed partial class SelectLanguage: ComponentBase
{
    [Parameter] public string ActiveLanguage { get; set; } = "Markdown";
    [Parameter] public EventCallback<string> ActiveLanguageChanged { get; set; }
    private string SearchString { get; set; } = string.Empty;
    public List<string> Languages { get; } = new() {
        "C", "C++", "CSS", "HTML", "Java", "JavaScript", "JSON", "JSX", "MariaDB SQL", "Markdown", "MS SQL",
        "MySQL", "PHP", "PostgreSQL", "Python", "Rust", "SQL", "SQLite", "TSX", "TypeScript", "XML", "C#", 
        "CMake", "Cython", "Dart", "Dockerfile", "Erlang", "Fortran", "F#", "Go", "Groovy", "Haskell", "HTTP", 
        "Jinja2", "Kotlin", "LESS", "Lua", "Nginx", "Objective-C", "Objective-C++", "Pascal", "Perl", "PowerShell", 
        "Ruby", "Sass", "SCSS", "Shell", "Swift", "sTeX", "LaTeX", "TOML", "VB.NET", "YAML",
    };
    
    private void FilterItems(ChangeEventArgs e) => SearchString = e.Value.ToString();
    private void SwitchLanguage(string lang)
    {
        ActiveLanguage = lang;
        ActiveLanguageChanged.InvokeAsync(ActiveLanguage);
    }
}