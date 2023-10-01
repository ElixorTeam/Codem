using Microsoft.AspNetCore.Components;
namespace WebClient.Shared.Components;

public partial class CreateSnippetForm : ComponentBase
{
    private List<string> Languages { get; set; }
    private string ActiveLanguage { get; set; }

    public CreateSnippetForm()
    {
        ActiveLanguage = "Markdown";
        Languages = new() {
            "C", "C++", "CSS", "HTML", "Java", "JavaScript", "JSON", "JSX", "MariaDB SQL", "Markdown", "MS SQL",
            "MySQL", "PHP", "PostgreSQL", "Python", "Rust", "SQL", "SQLite", "TSX", "TypeScript", "XML", "C#", 
            "CMake", "Cython", "Dart", "Dockerfile", "Erlang", "Fortran", "F#", "Go", "Groovy", "Haskell", "HTTP", 
            "Jinja2", "Kotlin", "LESS", "Lua", "Nginx", "Objective-C", "Objective-C++", "Pascal", "Perl", "PowerShell", 
            "Ruby", "Sass", "SCSS", "Shell", "Swift", "sTeX", "LaTeX", "TOML", "VB.NET", "YAML",
        };
        Languages.Sort();
    }
}