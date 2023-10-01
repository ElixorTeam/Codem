using Microsoft.AspNetCore.Components;
namespace WebClient.Shared.Components;

public partial class CreateSnippetForm : ComponentBase
{
    private List<string> Languages { get; set; }
    private List<string> ExpireTimeList { get; set; }
    private string ActiveLanguage { get; set; }
    private string SelectedExpireTime { get; set; }
    private string Title { get; set; }
    private string Password { get; set; }
    private bool IsPrivate { get; set; }
    private bool HasPassword { get; set; }
    

    public CreateSnippetForm()
    {
        Languages = new() {
            "C", "C++", "CSS", "HTML", "Java", "JavaScript", "JSON", "JSX", "MariaDB SQL", "Markdown", "MS SQL",
            "MySQL", "PHP", "PostgreSQL", "Python", "Rust", "SQL", "SQLite", "TSX", "TypeScript", "XML", "C#", 
            "CMake", "Cython", "Dart", "Dockerfile", "Erlang", "Fortran", "F#", "Go", "Groovy", "Haskell", "HTTP", 
            "Jinja2", "Kotlin", "LESS", "Lua", "Nginx", "Objective-C", "Objective-C++", "Pascal", "Perl", "PowerShell", 
            "Ruby", "Sass", "SCSS", "Shell", "Swift", "sTeX", "LaTeX", "TOML", "VB.NET", "YAML",
        };
        Languages.Sort();
        ActiveLanguage = "Markdown";
        
        ExpireTimeList = new List<string>() {
            "Never","1 hour", "1 day", "1 week", "1 month"
        };
        SelectedExpireTime = "Never";
        
        Title = string.Empty;
        Password = string.Empty;
        IsPrivate = false;
        HasPassword = false;
    }
}